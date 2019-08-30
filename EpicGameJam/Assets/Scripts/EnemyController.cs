using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float sensitivity = 3;

    public Transform rayCaster;

    public LayerMask layerMask;

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

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
    {
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
                agent.SetDestination(playerPos);
            }
        }
        if (distance < range * 0.5f)
        {
            Vector3 newPos = transform.position + (-dir * 3);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(newPos, out hit, 3.5f, NavMesh.AllAreas))
            {
                Vector3 result = hit.position;
                agent.SetDestination(result);
            }
        }
        else
        {
            RaycastHit hitInfo;
            float radius = 1;
            bool hit = Physics.SphereCast(rayCaster.position, radius, dir, out hitInfo, range, layerMask);
            
            // LOS
            if (hit && hitInfo.transform == PlayerController.instance.transform)
            {
                agent.isStopped = true;

                // add prediction
                float predictionTime = prediction * (distance / range);
                Vector3 predictedDir = ((playerPos + PlayerController.instance.prediction * predictionTime) - transform.position).normalized;

                if (Time.time > lastAttack + (1/attackSpeed))
                    Shoot(new Vector2(predictedDir.x, predictedDir.z));
            }
            else
            {
                if (Vector3.Distance(playerPos, agent.destination) > sensitivity / 2)
                {
                    agent.isStopped = false;
                    agent.SetDestination(playerPos);
                }
            }
        }
    }

    
    protected void Shoot (Vector2 dir)
    {
        lastAttack = Time.time;

        // add randomness
        float angle = Random.Range(-randomness/2, randomness/2);
        Quaternion quat = Quaternion.Euler(0, 0, angle);
        dir = quat * dir;

        GameObject projectile = Instantiate(projectilePrefab, rayCaster.position, rayCaster.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, 0, dir.y) * projectileSpeed, ForceMode.Impulse);
    }
}
