using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float sensitivity = 3;

    public Transform rayCaster;

    public LayerMask layerMask;

    public float range = 15f;

    public GameObject projectilePrefab;

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
        Vector2 dir = playerPos2 - pos2;

        if (dir.magnitude > range * 0.8f)
        {
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
            {
                agent.isStopped = false;
                agent.SetDestination(playerPos);
            }
        }
        else
        {
            dir.Normalize();
            // LOS
            if (Physics.Raycast(transform.position, dir, range, layerMask))
            {
                agent.isStopped = true;
                if (Time.time > lastAttack + 1)
                    Shoot(dir);
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
        float angle = Random.Range(-20, 20);
        Quaternion quat = Quaternion.Euler(0, 0, angle);
        dir = quat * dir;
        GameObject projectile = Instantiate(projectilePrefab, rayCaster.position, rayCaster.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x, 0, dir.y) * 10, ForceMode.Impulse);
    }
}
