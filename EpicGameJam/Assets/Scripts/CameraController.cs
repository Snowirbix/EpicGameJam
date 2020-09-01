using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FPS camera
 * rotate the parent for the y axis
 * rotate self for the x axis
 */
public class CameraController : MonoBehaviour
{
    private void Start ()
    {
        SetCursorLock(true);
    }

    private void Update ()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetMouseButtonDown((int)InputController.MouseButton.Left))
            {
                SetCursorLock(true);
            }
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3   selfRot = transform.localEulerAngles;
        Vector3 parentRot = transform.parent.localEulerAngles;

          selfRot.x -= y; // 2d  vertical  is for 3d x axis
        parentRot.y += x; // 2d horizontal is for 3d y axis

        transform.localEulerAngles = selfRot;
        transform.parent.localEulerAngles = parentRot;
    }

    protected void SetCursorLock (bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
