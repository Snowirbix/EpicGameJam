using UnityEngine;
using UnityEngine.AI;

public class JumperController : MonoBehaviour
{
    public float sensitivity = 3;

    public float range = 3f;

    public float attackSpeed = 0.5f;

    public float attackTime = 0.6f;

    public float jumpCd = 3;
    public AnimationCurve jumpHeight;

    protected float jumpTime;

    protected Vector3 jumpDirection;

    public LayerMask layerMask;
    public LayerMask playerLayer;

    protected bool isJumping = false;

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

    protected Rigidbody rb;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        attack = attackObject.GetComponent<ZombieAttack>();
        attack.Render(false);
        rb = GetComponent<Rigidbody>();
    }

    private void Update ()
    {
        if (isJumping)
        {
            if (Time.time > jumpTime + 1)
            {
                isJumping = false;
            }
            else
            {
                Vector3 direction = jumpDirection;
                direction.y = jumpHeight.Evaluate(Time.time - jumpTime);
                transform.position += direction * Time.deltaTime * agent.speed * 3;
                return;
            }
        }

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
        if (distance > 15)
        {
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
            {
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.SetDestination(playerPos);
            }
        }
        else if (distance > 10)
        {
            RaycastHit sphereHitInfo;
            float radius = 1;
            bool sphereHit = Physics.SphereCast(transform.position + Vector3.up, radius, dir, out sphereHitInfo, 15, layerMask);
            
            // LOS
            if (sphereHit && ((1 << sphereHitInfo.transform.gameObject.layer) & playerLayer) != 0)
            {
                if (Time.time > jumpTime + jumpCd)
                    Jump();
            }
            else
            {
                Debug.Log(sphereHitInfo.transform.name);
                if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
                {
                    agent.isStopped = false;
                    agent.updateRotation = true;
                    agent.SetDestination(playerPos);
                }
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

    protected void Jump ()
    {
        isJumping = true;
        agent.isStopped = true;
        agent.updateRotation = false;
        jumpDirection = (PlayerController.instance.transform.position - transform.position).normalized;
        animator.SetTrigger("jump");
        jumpTime = Time.time;
    }
}
