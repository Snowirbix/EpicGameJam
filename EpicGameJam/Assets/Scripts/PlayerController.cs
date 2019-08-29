using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public static PlayerControls controls;
    protected CharacterController character;

    [ReadOnly]
    public Vector2 axes;

    [Range(1, 5)]
    public float speed = 2f;

    public Transform rotator;

    public Pause pauseMenu;

    private Vector2 lastDirection = new Vector2(0,1);

    private bool canInteract = false;
    
    private InteractableScript interactableScript;

    private InformationBox informationBox;

    [HideInInspector]
    public Vector3 prediction;

    private void Awake ()
    {
        instance = this;

        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => axes = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled  += ctx => axes = Vector2.zero;
        controls.Gameplay.Interact.performed += ctx => Interact();
        controls.Gameplay.Pause.performed += ctx => pauseMenu.PauseGame();
    }

    private void OnEnable ()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable ()
    {
        controls.Gameplay.Disable();
    }

    private void Start ()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update ()
    {
        if(!Pause.gameIsPaused)
        {
            prediction = new Vector3(axes.x, 0, axes.y) * speed;
            // gravity if the player is above the ground
            prediction.y = transform.position.y > 0.2f ? -1 : 0;
            character.Move(prediction * Time.deltaTime);
            Direction();
            rotator.rotation = Quaternion.AngleAxis(Mathf.Atan2(lastDirection.x, lastDirection.y) * Mathf.Rad2Deg, Vector3.up);
        }
    }

    private void Direction()
    {
        Vector2 direction = new Vector2(axes.x,axes.y);

        if(direction == Vector2.zero)
        {
            return;
        }

        lastDirection = direction;
    }

    private void Interact()
    {
        if(!canInteract || Pause.gameIsPaused)
        {
            return;
        }

        interactableScript.Interact();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "interactable")
        {
            canInteract = true;
            interactableScript = other.GetComponent<InteractableScript>();
            informationBox = InformationBox.instance.Display(new InformationBox.Information(new [] {"Press 'E' to interact !"}));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "interactable")
        {
            canInteract = false;
            interactableScript = null;
            informationBox.Hide();
        }
    }
}
