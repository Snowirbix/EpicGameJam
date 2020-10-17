using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController instance;

    public Transform cameraRotator;
    public new Camera camera;

    public float sensitivity = 10f;
    public float speed = 1f;

    protected PlayerControls controls;
    protected CharacterController character;

    protected Vector2 lookAround = Vector2.zero;
    protected bool canInteract = false;
    protected InteractableScript interactableScript;
    protected InformationBox informationBox;

    private void Awake()
    {
        instance = this;

        controls = new PlayerControls();

        controls.Grounded.CaptureMouse.performed += CaptureMouse_performed;
        controls.Grounded.ReleaseMouse.performed += ReleaseMouse_performed;
        controls.Grounded.Interact.performed += Interact_performed;
    }

    private void Start ()
    {
        character = this.Q<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Grounded.Enable();
    }
    
    private void OnDisable()
    {
        controls.Grounded.Disable();
    }

    private void Update ()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Move();
            LookAround();
        }
    }

    protected void Move()
    {
        Vector2 input = controls.Grounded.Move.ReadValue<Vector2>();

        Vector3 forwardXZ = Vector3.ProjectOnPlane(camera.transform.forward, transform.up).normalized;

        Vector3 direction = Quaternion.LookRotation(forwardXZ) * input.x0y();
        
        character.Move(direction * Time.deltaTime * speed);
    }

    protected void LookAround()
    {
        Vector2 input = controls.Grounded.Look.ReadValue<Vector2>();

        lookAround += input * Mathf.Deg2Rad * sensitivity;
        lookAround.y = Mathf.Clamp(lookAround.y, -89f, 89f);

        cameraRotator.localRotation      = Quaternion.AngleAxis(lookAround.x, Vector3.up);
        camera.transform.localRotation   = Quaternion.AngleAxis(-lookAround.y, Vector3.right);
    }

    private void CaptureMouse_performed(InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ReleaseMouse_performed(InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if(!canInteract || Pause.gameIsPaused)
        {
            return;
        }

        interactableScript.Interact();
    }
    
    public void HideBox()
    {
        canInteract = false;
        interactableScript = null;
        informationBox.Hide();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "interactable")
        {
            canInteract = true;
            interactableScript = other.gameObject.Q<InteractableScript>();
            informationBox = InformationBox.instance.Display(new InformationBox.Information(new [] {"Press 'E' to interact !"}));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "interactable")
        {
            canInteract = false;
            interactableScript = null;
            InformationBox.instance.Hide();
        }
    }

}
