using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
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
    public UnityEvent onDashAnimate;


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
     float rotationSpeed = 10f;

        movement = playerMovement.ReadValue<Vector2>();

        // Calculate the movement direction
        movementDirection=Vector3.zero;
        movementDirection = new Vector3(movement.x, 0f, movement.y);
       // movementDirection = transform.TransformDirection(movementDirection);
       
        movementDirection.Normalize();
        Vector3 direction = movementDirection + new Vector3(0f, rb.linearVelocity.y, 0f).normalized;
        transform.TransformDirection(movementDirection);

        rb.linearVelocity = direction * speed;
        //rb.linearVelocity = movementDirection * speed;
        if (movementDirection != Vector3.zero) 
        {
            latestMovementDirection = movementDirection;
            //transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection, Vector3.up), rotationSpeed * Time.deltaTime);

        }
        // Clamp the movement speed
       // float movementSpeed = Mathf.Clamp(movement.magnitude, 0f, speed);

        // Move the player using Rigidbody
        //Debug.Log("speed " + movementSpeed);
        
        


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
            StartCoroutine(Dash(latestMovementDirection));
            //textCooldown.gameObject.SetActive(true);
            //cooldownTimer=AttackCooldown;
        }
    }


    public IEnumerator Dash(Vector3 dir)
    {
        canDash = false;
        isDashing = true;
        if (onDashAnimate!=null) onDashAnimate.Invoke();
        Debug.Log("Dashing " + dir);
       // dir.y = 3f;
        rb.AddForce(dir * dashSpeed, ForceMode.Impulse);
        
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
        yield return new WaitForSeconds(AttackCooldown);
        canDash = true;
        //GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
    }
}
