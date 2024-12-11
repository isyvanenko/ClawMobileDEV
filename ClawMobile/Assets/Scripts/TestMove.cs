using UnityEngine;

public class ClawController2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float descendSpeed = 2f; // Speed of vertical movement
    public float minX = -5f; // Minimum x position for claw
    public float maxX = 5f;  // Maximum x position for claw
    public float minY = -3f; // Minimum y position for claw
    public float maxY = 3f;  // Maximum y position for claw

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Horizontal movement
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

        // Vertical movement
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > minY)
                transform.Translate(Vector3.down * descendSpeed * Time.deltaTime);
        }
        else if (transform.position.y < maxY)
        {
            transform.Translate(Vector3.up * descendSpeed * Time.deltaTime);
        }

        // Clamp the position to the defined limits
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX), // Clamp horizontal position
            Mathf.Clamp(transform.position.y, minY, maxY), // Clamp vertical position
            transform.position.z  // Keep Z position unchanged
        );

        // Reset position if needed
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = startPosition;
        }
    }
}

