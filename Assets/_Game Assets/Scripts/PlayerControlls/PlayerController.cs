using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public PlayerInputManager playerinput;
    public int PlayerIndex=0;
    public ClassicProgressBar DashProgressBar;
    public bool UseForce = false;
    private StaminaScript staminaBar;


    bool isDashing = false;
    bool canDash = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // playerinput.JoinPlayer();
        latestMovementDirection =Vector3.zero;
    }

    
    
    private void Awake()
    {

        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount > 1)
        {
            PlayerIndex = playerCount - 1;
        }

        List<StaminaScript> StaminaBars = new List<StaminaScript>();
        StaminaBars=FindObjectsByType<StaminaScript>(FindObjectsSortMode.None).ToList<StaminaScript>();

        if (PlayerIndex < StaminaBars.Count) 
        {
            if (StaminaBars[PlayerIndex] != null) staminaBar = StaminaBars[PlayerIndex]; 
        }


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

    private void HandleMovement() 
    {
        float rotationSpeed = 10f;

        if (isDashing)
        {
            
            return;
        }
        movement = playerMovement.ReadValue<Vector2>();
        
        //Debug.Log(movement);

        // Calculate the movement direction
        movementDirection = new Vector3(movement.x, rb.linearVelocity.y, movement.y);
        movementDirection.Normalize();
        //rb.linearVelocity = movementDirection * speed;
        if (UseForce)
        {
            rb.AddForce(movementDirection * speed, ForceMode.Impulse);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 8f);
        }
        else 
        {
            rb.AddForce(movementDirection * speed, ForceMode.Force);
        }
          
        // rb.AddForce(movementDirection*speed,ForceMode.Force);
        if (movementDirection != Vector3.zero) 
        {
            latestMovementDirection = movementDirection;
            Vector3 orientation = movementDirection;
            orientation.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(orientation, Vector3.up), rotationSpeed * Time.deltaTime);

        }
      
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    public void StartDash()
    {
        //Debug.Log("Dashing " + canDash);
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
       // Debug.Log("Dashing " + dir);
        // dir.y = 3f;
        rb.AddForce(dir * dashSpeed, ForceMode.VelocityChange);
       if(staminaBar!=null) staminaBar.SetProgress(0f,0.1f);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        if (staminaBar != null) staminaBar.SetProgress(1f, AttackCooldown);
        yield return new WaitForSeconds(AttackCooldown);
        canDash = true;
        //GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
    }
}
