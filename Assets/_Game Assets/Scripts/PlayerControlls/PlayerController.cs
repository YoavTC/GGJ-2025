using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public float speed;
    public InputAction playerMovement;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        playerMovement = context.action;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() 
    {
        /*
        movement = playerMovement.ReadValue<Vector2>();
        Debug.Log(movement);
        Vector3 dir =new Vector3(movement.x, 0f, movement.y);//= Vector3.zero;

        
        dir.Normalize();

        rb.linearVelocity=speed*dir*Time.deltaTime;*/
        // /rb.Move(dir)
        // rb.AddForce(dir * speed,ForceMode.VelocityChange);

        movement = playerMovement.ReadValue<Vector2>();

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(movement.x, 0f, movement.y);
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection.Normalize();

        // Clamp the movement speed
        float movementSpeed = Mathf.Clamp(movement.magnitude, 0f, speed);

        // Move the player using Rigidbody
        rb.linearVelocity = movementDirection * movementSpeed;
    }

    private void FixedUpdate()
    {
        //HandleMovement();
    }
}
