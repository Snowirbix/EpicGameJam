using UnityEngine;

public class Delivery : MonoBehaviour
{
    public Transform[] targets;

    [ReadOnly]
    public int currentTarget = 0;

    public GameObject arrowDirection;

    public void Next ()
    {
        currentTarget++;
        if (currentTarget == targets.Length)
        {
            // Start new phase
            enabled = false;
            arrowDirection.SetActive(false);
            Spawner.instance.StartWaves();
        }
    }

    private void Update ()
    {
        if(targets.Length != 0)
        {
            arrowDirection.SetActive(true);
            Vector3 direction = targets[currentTarget].position - transform.position;
            arrowDirection.transform.localRotation = Quaternion.AngleAxis(Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, Vector3.up); 
        }
        else
        {
            arrowDirection.SetActive(false);
        }
    }
}
