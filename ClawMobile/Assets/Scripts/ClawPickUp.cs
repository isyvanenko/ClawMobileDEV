using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPickUp : MonoBehaviour
{   
     public Transform attachmentPoint; // Reference to the attachment point on the claw
    private GameObject detectedObject; // Object currently in the claw's range
    private GameObject pickedObject;   // Object currently picked up by the claw
    public Vector3 ropeStartPosition; // Starting position of the rope
    public float moveSpeed = 5f;      // Speed of horizontal movement
    public GameObject RopeSystem;

    public ClawController2D clawController2D;

    public bool canMove = true;
    private bool canPickUpAgain = true;

    void Start()
    {
        clawController2D = GameObject.FindGameObjectWithTag("ClawController2D").GetComponent<ClawController2D>();
        if(clawController2D == null) Debug.LogError("Assign clawController to the ClawPickUp");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is pickable (use a tag to identify pickable objects)
        if (other.CompareTag("Pickable"))
        {
            detectedObject = other.gameObject; // Save reference to the detected object
        }
    }

    void Update()
    {
        if(canPickUpAgain)
        {
            if (Input.GetKeyDown(KeyCode.E) && detectedObject != null && pickedObject == null)
        {
            // Generate a random chance (50%)
            bool success = Random.value > 0.5f;

            if (success)
            {
                    // Attach the detected object to the claw
                    
                    StartCoroutine(FailPickUp());
                }
            else
            {
            
                Debug.Log("Pickup failed. Claw reset.");
                StartCoroutine(FailPickUp());
            }
        }

        }
        // If the player presses E and there is a detected object
        
        // Release the object (if any) when pressing Space
        if (Input.GetKeyDown(KeyCode.Space) && pickedObject != null)
        {
            ReleaseObject();
        }
    }

    private void ReleaseObject()
    {
        // Detach the object from the claw
        pickedObject.transform.SetParent(null);
        pickedObject.GetComponent<Rigidbody2D>().isKinematic = false; // Re-enable physics
        pickedObject = null;
    }
    private void PickUpObject()
    {
        pickedObject = detectedObject;
        pickedObject.transform.SetParent(attachmentPoint); // Make it a child of the claw
        pickedObject.transform.localPosition = Vector3.zero; // Snap to the attachment point
        pickedObject.GetComponent<Rigidbody2D>().isKinematic = true; // Disable physics
        Debug.Log("Object picked up!");
    }

    public void PickUpFunction()
    {
        Debug.Log("PRESSING");
        if(canPickUpAgain == true)
        {
            Debug.Log("Button PickUp");
            // Generate a random chance (50%)
            bool success = Random.value > 0.5f;

                if (success)
                {
                    // Attach the detected object to the claw
                    
                    Debug.Log("Succesful syka");
                }
                      else
                      {
            
                          Debug.Log("Pickup failed. Claw reset.");
                           StartCoroutine(FailPickUp());
                    }
         
        }
    }
IEnumerator FailPickUp()
{

    PickUpObject();

    canMove = false;
    clawController2D.StartMovingUp();
    canPickUpAgain = false;


    yield return new WaitForSeconds(3);

    ReleaseObject();
    
    Debug.Log("Finished Coroutine");
    canMove = true;
    
    canPickUpAgain = true;


}

}