using UnityEngine;
using UnityEngine.AI;

public class JumperController : MonoBehaviour
{
    public float sensitivity = 3;

    public float range = 2f;

    public float attackSpeed = 0.3f;

    public float attackTime = 0.4f;

    [Range(0, 2)]
    public float prediction = 1;

    [Range(0, 40)]
    public float randomness = 20;

    protected NavMeshAgent agent;

    protected float lastAttack;

    public Transform attackObject;

    protected ZombieAttack attack;

    protected bool isAttacking = false;
    protected bool hasEmitted = true;

    public Animator animator;

    protected float animSpeed = 0;

    public ParticleSystem particles;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        attack = attackObject.GetComponent<ZombieAttack>();
        attack.Render(false);
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

        if (isAttacking && Time.time > lastAttack + attackTime)
        {
            StopAttack();
        }
        if (isAttacking && hasEmitted == false && Time.time > lastAttack + attackTime - 0.2f)
        {
            particles.Play();
            hasEmitted = true;
        }
        if (distance > 10)
        {
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
            {
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.SetDestination(playerPos);
            }
        }
        else if (distance > range)
        {
            // hypersensitivity
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity/10f)
            {
                agent.isStopped = false;
                agent.updateRotation = false;
                agent.SetDestination(playerPos);
            }
        }
        else
        {
            agent.isStopped = true;
            agent.updateRotation = false;

            if (Time.time > lastAttack + (1/attackSpeed))
                StartAttack();
        }

        if (!agent.updateRotation && !isAttacking)
        {
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir2.x, dir2.y) * Mathf.Rad2Deg, Vector3.up);
        }
    }

    protected void StartAttack ()
    {
        isAttacking = true;
        hasEmitted = false;
        lastAttack = Time.time;
        attack.Render(true);

        // add randomness
        float angle = Random.Range(-randomness/2, randomness/2);
        Quaternion quat = Quaternion.Euler(0, angle, 0);
        transform.localRotation *= quat;

        animator.SetTrigger("attack");
    }

    protected void StopAttack ()
    {
        attack.Render(false);
        attack.Attack();

        isAttacking = false;
    }
}
