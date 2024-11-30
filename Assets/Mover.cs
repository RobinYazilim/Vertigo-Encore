using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private InputAction moveAction;
    private Rigidbody2D rb;
    public Camera cam;
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
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(rb.position.x, rb.position.y, -10), 0.2f);
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
        TeleportHelper teleporthelper = collision.gameObject.GetComponent<TeleportHelper>();

        if (trigger != null)
        {
            dialogueManager.StartDialogue(trigger.GetDialogue());
        }
        if (teleporthelper != null)
        {
            transform.position = teleporthelper.ToPos.transform.position;
            cam.transform.position = teleporthelper.ToPos.transform.position;
        }
    }
}
