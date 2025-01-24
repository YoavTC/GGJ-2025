using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] [Layer] private string groundLayerMask;
    private int _groundLayerMask;

    private void Start()
    {
        _groundLayerMask = LayerMask.GetMask(groundLayerMask);
    }

    public void FixedUpdate()
    {
        
        bool isAirborne = !Physics.CheckSphere(groundCheck.position, groundCheckRadius, _groundLayerMask);
        bool isRunning = (Math.Floor(Mathf.Abs(rb.linearVelocity.x) * 10 / 10)> 0f) && !isAirborne; 
        
        animator.SetBool("is_running", isRunning);
        animator.SetBool("is_airborne", isAirborne);
    }

    public void OnDash()
    {
        animator.SetBool("is_leaping", true);
    }
}
