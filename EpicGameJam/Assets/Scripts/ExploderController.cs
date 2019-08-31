using UnityEngine;
using UnityEngine.AI;

public class ExploderController : MonoBehaviour
{
    public float sensitivity = 3;

    public float range = 5f;

    public float expRadius = 5f;

    protected NavMeshAgent agent;

    protected float lastAttack = 0;

    public float attackTime = 3;

    protected bool isAttacking = false;

    public Animator animator;

    protected float animSpeed = 0;

    public ParticleSystem[] particles;

    public Material eyes;

    public Light light;

    protected Color emissionColor;

    [ReadOnly]
    public float ratio;

    public AnimationCurve intensityCurve;

    [ReadOnly]
    public float intensity;

    public float lightIntensity = 2f;

    protected Health health;

    public GameObject impact;

    private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        emissionColor = eyes.GetColor("_EmissionColor");
        health = GetComponent<Health>();
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

        if (isAttacking)
        {
            ratio = (Time.time - lastAttack) / attackTime;
            intensity = intensityCurve.Evaluate(ratio);
            //eyes.SetColor("_EmissionColor", emissionColor * intensity);
            light.intensity = intensity * lightIntensity;

            if (ratio >= 1)
            {
                StopAttack();
            }
        }


        Vector3 playerPos = PlayerController.instance.transform.position;
        Vector3 dir = playerPos - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();

        if (distance > 10)
        {
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity)
            {
                agent.SetDestination(playerPos);
            }
        }
        else
        {
            // hypersensitivity
            if (Vector3.Distance(playerPos, agent.destination) > sensitivity/10f)
            {
                agent.SetDestination(playerPos);
            }
        }

        if (distance < range)
        {
            if (!isAttacking && lastAttack == 0)
                StartAttack();
        }
    }

    protected void StartAttack ()
    {
        isAttacking = true;
        lastAttack = Time.time;
    }

    protected void StopAttack ()
    {
        isAttacking = false;
        health.ChangeHealth(-1000);
        foreach (ParticleSystem ps in particles)
        {
            ps.Play();
        }
        float dist = Vector3.Distance(PlayerController.instance.transform.position, transform.position);
        if (dist < expRadius)
        {
            float damage = - Mathf.Lerp(40, 0, dist / expRadius);
            PlayerController.instance.ChangeHealth(damage);
            Debug.Log("Exp " + damage);
            Instantiate(impact, PlayerController.instance.RayCaster.position, PlayerController.instance.RayCaster.rotation, PlayerController.instance.RayCaster);
        }
    }
}
