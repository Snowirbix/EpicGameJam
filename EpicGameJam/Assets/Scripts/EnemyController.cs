using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float sensitivity = 3;

    protected NavMeshAgent agent;

    protected Vector3 lastDest = Vector3.zero;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
    {
        Vector3 p = PlayerController.instance.transform.position;

        if (Vector3.Distance(p, lastDest) > sensitivity || Vector3.Distance(p, transform.position) < 10)
        {
            agent.SetDestination(p);
        }
    }
}
