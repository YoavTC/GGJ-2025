using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 90;
    public float leanAngle = 15f; // Maximum lean angle for side rotation
    public float leanSmoothness = 5f; // Smoothness of the lean effect

    private Rigidbody rb;
    private float currentLeanAngle = 0f; // Tracks the current lean angle

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for horizontal and vertical movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotate the player
        transform.Rotate(Vector3.up * moveHorizontal * rotationSpeed * Time.deltaTime);

        // Calculate movement in the horizontal plane only
        Vector3 movement = transform.forward;
        movement.y = 0; // Remove vertical component
        movement.Normalize(); // Normalize to maintain consistent speed
        movement *= moveVertical * moveSpeed;

        // Apply movement
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        // Handle lean effect for horizontal movement
        HandleLean(moveHorizontal);
    }

    void HandleLean(float moveHorizontal)
    {
        float targetLeanAngle = moveHorizontal * -leanAngle; // Negative for correct leaning direction

        // Smoothly interpolate to the target lean angle
        currentLeanAngle = Mathf.Lerp(currentLeanAngle, targetLeanAngle, Time.deltaTime * leanSmoothness);

        // Apply lean rotation to the player's local Z-axis
        Quaternion leanRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, currentLeanAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, leanRotation, Time.deltaTime * leanSmoothness);
    }
}
