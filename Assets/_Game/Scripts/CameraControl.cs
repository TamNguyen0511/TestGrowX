using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset = new Vector3(0, 5, -10); 
    public float smoothTime = 0.3f; 

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Calculate target position
        Vector3 targetPosition = player.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
