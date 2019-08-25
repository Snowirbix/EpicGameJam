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

    private void Awake ()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => axes = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled  += ctx => axes = Vector2.zero;
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
        MessageBox.instance.Display(new MessageBox.Message("Mr Bean", new [] { "Hey, do you want to play a game ?", "Give me your snack !" }, 3));
    }

    private void Update ()
    {
        character.Move(new Vector3(axes.x, 0, axes.y) * speed * Time.deltaTime);
    }
}
