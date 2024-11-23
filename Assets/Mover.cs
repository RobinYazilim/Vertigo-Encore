using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private InputAction moveAction;
    private Rigidbody2D rb;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Player/Walk"];

        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();

        Vector2 moveVector = movement * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveVector);
        //rb.linearVelocity = moveVector;
    }

    // Optional: Check for collisions with other colliders (if using triggers)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle logic when colliding with another trigger collider
        Debug.Log("Triggered with: " + other.gameObject.name);
    }

    // Optional: Check for collisions if not using triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle logic when colliding with another collider
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
