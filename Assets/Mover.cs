using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private InputAction moveAction;
    private Rigidbody2D rb;
    public DialogueManager dialogueManager;
    public bool isAvailable;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Player/Walk"];

        rb = GetComponent<Rigidbody2D>();
        isAvailable = true;
    }


    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (!isAvailable)
        {
            return;
        }
        Vector2 movement = moveAction.ReadValue<Vector2>();

        Vector2 moveVector = movement * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAvailable)
        {
            return;
        }


        Debug.Log("Collided with: " + collision.gameObject.name);
        DialogueTrigger trigger = collision.gameObject.GetComponent<DialogueTrigger>();

        if (trigger != null)
        {
            dialogueManager.StartDialogue(trigger.GetDialogue());
        }

    }
}
