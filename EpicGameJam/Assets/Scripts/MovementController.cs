using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    protected new Rigidbody rigidbody;
    
    public float speed = 5f;

    public bool grounded = false;

    private void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /**
     * <param name="direction">normalized direction on ground plane</param>
     */
    public void Move (Vector2 direction)
    {
        direction *= speed;
        rigidbody.velocity = new Vector3(direction.x, rigidbody.velocity.y, direction.y);
    }

    public void Jump (float jumpForce)
    {
        rigidbody.velocity += Vector3.up * jumpForce;
    }
}
