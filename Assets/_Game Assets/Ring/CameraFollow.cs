using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -8); // Default offset (adjustable in the Inspector)
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    public float verticalAngle = 20f; // Angle to tilt the camera downward

    void LateUpdate()
    {
        // Calculate the camera's desired position relative to the player
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Smoothly interpolate between current and desired positions
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Rotate the camera to look at the player, but tilt it downward slightly
        transform.LookAt(player.position + Vector3.up * -0.5f);

        // Apply a downward tilt to simulate a third-person perspective
        transform.rotation = Quaternion.Euler(verticalAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
