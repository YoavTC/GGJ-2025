using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public float speed;
    public float dashSpeed;
    public float dashDuration;
    public float AttackCooldown;
    public Vector3 movementDirection;
    public Vector3 latestMovementDirection;
    public InputAction playerMovement;
    private Vector2 movement;


    bool isDashing = false;
    bool canDash = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        latestMovementDirection=Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        playerMovement = context.action;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartDash();
        }
    }

    // Update is called once per frame
    void Update()
    {
       // HandleMovement();
    }



    private void HandleMovement() 
    {
       

        movement = playerMovement.ReadValue<Vector2>();

        // Calculate the movement direction
        movementDirection=Vector3.zero;
        movementDirection = new Vector3(movement.x, 0f, movement.y);
       // movementDirection = transform.TransformDirection(movementDirection);
       transform.TransformDirection(movementDirection);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero) 
        {
            latestMovementDirection = movementDirection;
        }
        // Clamp the movement speed
       // float movementSpeed = Mathf.Clamp(movement.magnitude, 0f, speed);

        // Move the player using Rigidbody
        //Debug.Log("speed " + movementSpeed);
        rb.linearVelocity = movementDirection * speed;
        transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);


    }

    private void FixedUpdate()
    {
        HandleMovement();
    }


    public void StartDash()
    {
        Debug.Log("Dashing " + canDash);
        if (canDash)
        {
            StartCoroutine(Dash(movementDirection));
            //textCooldown.gameObject.SetActive(true);
            //cooldownTimer=AttackCooldown;
        }
    }


    public IEnumerator Dash(Vector3 dir)
    {
        canDash = false;
        isDashing = true;
        
        Debug.Log("Dashing " + dir);
        //dir.y = 10;
        rb.AddForce(dir * dashSpeed, ForceMode.Impulse);
        
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
        yield return new WaitForSeconds(AttackCooldown);
        canDash = true;
        //GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
    }
}
