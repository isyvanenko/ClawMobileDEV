using System;
using UnityEngine;

public class ClawController2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float descendSpeed = 2f; // Speed of vertical movement
    public float minX = -5f; // Minimum x position for claw
    public float maxX = 5f;  // Maximum x position for claw
    public float minY = -3f; // Minimum y position for claw
    public float maxY = 3f;  // Maximum y position for claw

    private float horizontalInput = 0f; // Variable to store horizontal movement direction


    //BOOLS to controll up and down movement
    private bool isMovingDown = false;
    public bool isMovingUp = false;

    public ClawPickUp isProcPickUp;

    private Vector3 startPosition;
    private float moveStep = 0.1f;

    //delete later
    public Vector2 testVector;



    public JoystickMovement joystick;      // Reference to the horizontal joystick
    

    void Start()
    {
        startPosition = transform.position;

        //delete later
        
    }

    void Update()
    {
        
       if(isProcPickUp.canMove == true)
       {
        ///////////////////////////////////////////////////////
        //PC testing

        /// -> CLAW LEFT, RIGHT
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

        /// -> CLAW GOES DOWN
        if (Input.GetKey(KeyCode.S))  { isMovingDown = true; }

        //PC testing
        /////////////////////////////////////////////////////////
        
         // Get horizontal input from the joystick
        float horizontalJoystic = joystick.horizontalInput;

        // Move the claw horizontally based on joystick input
        transform.Translate(Vector3.right * horizontalJoystic * moveSpeed * Time.deltaTime / 5);

        // Clamp the claw's position within horizontal boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX), // Clamp horizontal position
            transform.position.y,                         // Keep vertical position unchanged
            transform.position.z                          // Keep Z position unchanged
        );
        
        
       
        

        if (transform.position.y <= minY)
        {
            isMovingDown = false;
        }
       }
        

        if(isMovingDown)
        {
            Debug.Log("going down");
                transform.Translate(Vector3.down * descendSpeed * Time.deltaTime / 4);
        }

        if(isMovingUp)
        {
           MoveUp();
        }
        
       
       // if (transform.position.y < maxY)
       // {
       //     Debug.Log("transforming position y");
       //     
       // }
          
        
       

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

    public void StartMovingUp()
    {
        isMovingUp = true;
    }

    private void MoveUp()
    {
        if(transform.position.y < startPosition.y + testVector.y)
        {
            transform.Translate(Vector3.up * descendSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Test");
            isMovingUp = false;
        }
    }

    /////////////////////////////////////////////////////////
    //MOBILE CONTROLLS
     public void MoveLeft()
    {
        if(isProcPickUp.canMove == true)
        {
        Vector3 newPosition = transform.position;
        newPosition.x -= moveStep; // Decrease x position
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); // Clamp within boundaries
        transform.position = newPosition;
        }
        
    }

    // Function to start moving right
    public void MoveRight()
    {
        if(isProcPickUp.canMove == true)
        {
        Vector3 newPosition = transform.position;
        newPosition.x += moveStep; // Increase x position
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); // Clamp within boundaries
        transform.position = newPosition;
        }
        
    }

    public void MoveDown()
    {
        if(isProcPickUp.canMove == true)
        {
            isMovingDown = true;
        }
    }

    // Function to stop horizontal movement
    public void StopMoving()
    {
        horizontalInput = 0f;
    }
     //MOBILE CONTROLLS
    /////////////////////////////////////////////////////////
    ///

    public void ResetClawPosition()
    {
    isMovingDown = false; // Ensure downward movement stops
    isMovingUp = false;   // Stop upward movement
    //transform.position = new Vector3(transform.position.x, maxY, transform.position.z); // Reset to top position
    transform.position = startPosition;
    Debug.Log("Claw reset and ready to move down again.");
    }

}

