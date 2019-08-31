using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float sensitivity = 3;

    public Transform rayCaster;

    public LayerMask layerMask;
    public LayerMask playerLayer;

    public float range = 15f;

    public GameObject projectilePrefab;

    public float projectileSpeed = 15;

    public float attackSpeed = 1;

    [Range(0, 2)]
    public float prediction = 1;

    [Range(0, 40)]
    public float randomness = 20;

    protected NavMeshAgent agent;

    protected float lastAttack;

    public Animator animator;
    
    protected float animSpeed = 0;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
    {
        if (agent.isStopped)
        {
            animSpeed -= Time.deltaTime*5f;
        }
        else
        {
            animSpeed += Time.deltaTime*5f;
        }
        animSpeed = Mathf.Clamp01(animSpeed);
        animator.SetFloat("speed", animSpeed);

        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector2 playerPos2 = new Vector2(playerPos.x, playerPos.z);
        Vector2 pos2 = new Vector2(transform.position.x, transform.position.z);
        Vector3 dir = playerPos - transform.position;
        Vector2 dir2 = playerPos2 - pos2;
        float distance = dir2.magnitude;
        dir.Normalize();
        dir2.Normalize();

        if (distance > range * 0.9f)
        {
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
            {
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.SetDestination(playerPos);
                //Debug.Log("Move towards player");
            }
        }
        else if (distance < range * 0.5f)
        {
            RaycastHit sphereHitInfo;
            float radius = 1;
            bool sphereHit = Physics.SphereCast(rayCaster.position, radius, dir, out sphereHitInfo, range, layerMask);
            
            // LOS
            if (sphereHit && ((1 << sphereHitInfo.transform.gameObject.layer) & playerLayer) != 0)
            {
                Vector3 newPos = transform.position + (-dir * 4);
                NavMeshHit navHit;

                if (NavMesh.SamplePosition(newPos, out navHit, 4f, NavMesh.AllAreas))
                {
                    Vector3 result = navHit.position;
                    
                    RaycastHit rayHitInfo;
                    // allow the zombie to not move away if it loses the LOS
                    bool rayHit = Physics.SphereCast(result + Vector3.up, radius, dir, out rayHitInfo, range+4f, layerMask);
                    
                    // LOS
                    if (rayHit && ((1 << rayHitInfo.transform.gameObject.layer) & playerLayer) != 0)
                    {
                        agent.isStopped = false;
                        agent.updateRotation = true;
                        agent.SetDestination(result);
                        Debug.DrawRay(result + Vector3.up, playerPos - result, Color.blue, Time.deltaTime*2);
                    }
                    else
                    {
                        // what if it is close to the player but has no LOS
                        Debug.DrawRay(result + Vector3.up, playerPos - result, Color.red, Time.deltaTime*2);
                        agent.isStopped = true;
                        agent.updateRotation = false;
                        // add prediction
                        float predictionTime = prediction * (distance / range);
                        Vector3 predictedDir = ((playerPos + PlayerController.instance.prediction * predictionTime) - transform.position).normalized;

                        if (Time.time > lastAttack + (1/attackSpeed))
                            Shoot(new Vector2(predictedDir.x, predictedDir.z));
                    }
                }
                else
                {
                    Debug.Log("SamplePosition failed");
                }
            }
            else
            {
                if (playerPos != agent.destination)
                {
                    //Debug.Log("No LOS, moving forward");
                    agent.isStopped = false;
                    agent.updateRotation = true;
                    agent.SetDestination(playerPos);
                }
            }
        }
        else
        {
            RaycastHit hitInfo;
            float radius = 1;
            bool hit = Physics.SphereCast(rayCaster.position, radius, dir, out hitInfo, range, layerMask);
            
            // LOS
            if (hit && ((1 << hitInfo.transform.gameObject.layer) & playerLayer) != 0)
            {
                //Debug.Log("LOS, shooting");
                agent.isStopped = true;
                agent.updateRotation = false;

                // add prediction
                float predictionTime = prediction * (distance / range);
                Vector3 predictedDir = ((playerPos + PlayerController.instance.prediction * predictionTime) - transform.position).normalized;

                if (Time.time > lastAttack + (1/attackSpeed))
                    Shoot(new Vector2(predictedDir.x, predictedDir.z));
            }
            else
            {
                if (playerPos != agent.destination)
                {
                    //Debug.Log("No LOS, moving forward");
                    agent.isStopped = false;
                    agent.updateRotation = true;
                    agent.SetDestination(playerPos);
                }
            }
        }
        
        if (!agent.updateRotation)
        {
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir2.x, dir2.y) * Mathf.Rad2Deg, Vector3.up);
        }
    }

    
    protected void Shoot (Vector2 dir)
    {
        lastAttack = Time.time;

        // add randomness
        float angle = Random.Range(-randomness/2, randomness/2);
        Quaternion quat = Quaternion.Euler(0, 0, angle);
        dir = quat * dir;

        GameObject projectile = Instantiate(projectilePrefab, rayCaster.position, Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, Vector3.up));
        projectile.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, 0, dir.y) * projectileSpeed, ForceMode.Impulse);

        animator.SetTrigger("attack");
    }
}
