using UnityEngine;

public class From2Dto3D : MonoBehaviour
{
    public CameraSettings camera2D;
    public CameraSettings camera3D;
    public GameObject camera;

    public AnimationCurve ratioCurve;

    public float duration = 10f;

    [ReadOnly]
    public float ratio;

    protected float startTime;
    protected bool transforming = false;

    [System.Serializable]
    public struct CameraSettings
    {
        public Vector3 position;
        public float tilt;
        public float fov;
        public int pixelation;
    }

    private void Start ()
    {
        Application.targetFrameRate = 30;
    }
    
    public void StartTransformation ()
    {
        startTime = Time.time;
        transforming = true;
    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.T))
        {
            StartTransformation();
        }
        if (transforming)
        {
            ratio = Mathf.Min((Time.time - startTime) / duration, 1);
            
            float fov = Mathf.Lerp(camera2D.fov, camera3D.fov, ratioCurve.Evaluate(ratio));

            camera.GetComponent<Camera>().fieldOfView = fov;
            camera.transform.localPosition = camera3D.position.normalized * camera2D.position.magnitude / fov;
            //camera.transform.localRotation = Quaternion.Euler(Mathf.Lerp(camera2D.tilt, camera3D.tilt, ratio), 0, 0);
            camera.GetComponent<Pixelate>().pixelSizeX = (int)Mathf.Lerp(camera2D.pixelation, 1, ratio);
            camera.GetComponent<Pixelate>().pixelSizeY = (int)Mathf.Lerp(camera2D.pixelation, 1, ratio);

            if (ratio == 1)
            {
                camera.GetComponent<Pixelate>().enabled = false;
                transforming = false;
                Application.targetFrameRate = 60;
            }
        }
    }
}
