using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class From2Dto3D : MonoBehaviour
{
    public CameraSettings camera2D;
    public CameraSettings camera3D;
    public GameObject cam;

    public AnimationCurve ratioCurve;

    public float duration = 10f;

    [ReadOnly]
    public float ratio;

    protected float startTime;
    protected bool transforming = false;

    private FontChanger fontChanger;

    protected Pixelate pixelate;
    protected Camera camera;

    [System.Serializable]
    public struct CameraSettings
    {
        public Vector3 position;
        public float fov;
        public int pixelation;
    }

    private void Awake ()
    {
        Application.targetFrameRate = 30;
    }
    
    private void Start ()
    {
        fontChanger = GetComponent<FontChanger>();
        camera = cam.GetComponent<Camera>();
        pixelate = cam.GetComponent<Pixelate>();
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
            float r = Mathf.Max(ratio - 0.5f, 0) * 2f;
            
            float fov = Mathf.Lerp(camera2D.fov, camera3D.fov, ratioCurve.Evaluate(ratio));

            camera.fieldOfView = fov;
            Vector3 dir = Vector3.Lerp(camera2D.position.normalized, camera3D.position.normalized, ratio);
            camera.transform.localPosition = dir * camera2D.position.magnitude / fov;
            camera.transform.localRotation = Quaternion.LookRotation(-dir);
            int pixels = (int)Mathf.Lerp(camera2D.pixelation, 1, ratio);

            if (pixels == 1)
            {
                pixelate.enabled = false;
                camera.GetComponent<PostProcessLayer>().enabled = true;
            }
            else
            {
                pixelate.pixelSizeX = pixels;
                pixelate.pixelSizeY = pixels;
            }

            if (ratio == 1)
            {
                pixelate.enabled = false;
                camera.GetComponent<PostProcessLayer>().enabled = true;
                transforming = false;
                Application.targetFrameRate = 60;
                fontChanger.FontChange();
            }
        }
    }
}
