using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void Update()
    {
        animator.SetBool("is_running", Mathf.Abs(rb.linearVelocity.x) > 0f);
    }

    public void OnDash()
    {
        animator.SetTrigger("dash");
    }
}
