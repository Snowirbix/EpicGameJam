using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /**
     * <summary>Create vec2(x, z)</summary>
     */
    public static Vector2 XZ(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }

    /**
     * <summary>Create vec3(x, 0, y)</summary>
     */
    public static Vector3 Dir3(this Vector2 vec)
    {
        return new Vector3(vec.x, 0, vec.y);
    }

    /**
     * Query the component
     */
    public static T Q<T>(this GameObject obj)
    {
        return obj.GetComponent<T>();
    }

    public static Vector3 position(this GameObject obj)
    {
        return obj.transform.position;
    }

    public static Quaternion rotation(this GameObject obj)
    {
        return obj.transform.rotation;
    }

    public static Rigidbody SetVelocity(this Rigidbody rb, Vector3 velocity)
    {
        rb.velocity = velocity;
        return rb;
    }

    public static Rigidbody SetIsKinematic(this Rigidbody rb, bool isKinematic)
    {
        rb.isKinematic = isKinematic;
        return rb;
    }
}
