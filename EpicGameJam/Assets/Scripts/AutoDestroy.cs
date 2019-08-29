using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float ttl = 1;

    public float dist = 10;

    protected float startTime;
    protected Vector3 startPos;

    private void Start ()
    {
        startTime = Time.time;
        startPos = transform.position;
    }

    private void Update ()
    {
        if (ttl > 0 && Time.time > startTime + ttl)
        {
            Destroy(gameObject);
        }
        if (dist > 0 && Vector3.Distance(transform.position, startPos) > dist)
        {
            Destroy(gameObject);
        }
    }
}
