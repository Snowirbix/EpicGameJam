using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    protected PlayerControls controls;
    protected CharacterController character;

    [ReadOnly]
    public Vector2 axes;

    [Range(1, 5)]
    public float speed = 2f;

    public Transform rotator;

    private Vector2 lastDirection = new Vector2(0,1);

    private bool canInteract = false;
    
    private InteractableScript interactableScript;
    private void Awake ()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => axes = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled  += ctx => axes = Vector2.zero;
        controls.Gameplay.Interact.performed += ctx => Interact();
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
        MessageBox.instance.Display(new MessageBox.Message("Mr Bean", new [] { "Hey, do you want to play a game ?", "Give me your snack !" }, 1));
    }

    private void Update ()
    {
        character.Move(new Vector3(axes.x, 0, axes.y) * speed * Time.deltaTime);
        Direction();
        rotator.rotation = Quaternion.AngleAxis(Mathf.Atan2(lastDirection.x, lastDirection.y) * Mathf.Rad2Deg, Vector3.up);
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
        if(!canInteract)
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "interactable")
        {
            canInteract = false;
            interactableScript = null;
        }
    }
}
