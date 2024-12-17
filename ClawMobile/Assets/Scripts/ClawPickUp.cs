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

    private bool isReadyToPick = false; // New flag to check if claw is ready to pick up

    public GameObject RopeSystem;

    PrizeManager prizeManager;

    ClawController2D clawController2D;

    public bool canMove = true;
    private bool canPickUpAgain = true;

    void Start()
    {
        clawController2D = GameObject.FindGameObjectWithTag("ClawController2D").GetComponent<ClawController2D>();
        prizeManager = GameObject.FindGameObjectWithTag("PrizeManager").GetComponent<PrizeManager>();
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is pickable (use a tag to identify pickable objects)
        if (other.CompareTag("Pickable"))
    {
        detectedObject = other.gameObject; // Save reference to the detected object
        isReadyToPick = true; // Set flag to allow pickup
        Debug.Log("Detected a pickable object: " + detectedObject.name);
    }
    }

    void Update()
    {
        if(isReadyToPick && canPickUpAgain)
        {
            if (Input.GetKeyDown(KeyCode.E) && detectedObject != null && pickedObject == null)
        {
            // Generate a random chance (50%)
            bool success = Random.value > 1f;

            if (success)
            {
                    
                    Debug.Log("Pickup successful. Claw reset");
                    StartCoroutine(SuccessPickUp());
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
        if (isReadyToPick && canPickUpAgain)
    {
        Debug.Log("Attempting to pick up...");
        bool success = Random.value > 0.5f; // Random chance

        if (success)
        {
            Debug.Log("Pickup success");
            StartCoroutine(SuccessPickUp());
        }
        else
        {
            Debug.Log("Pickup failed");
            StartCoroutine(FailPickUp());
        }
    }
    else
    {
        Debug.LogWarning("Cannot pick up! No object detected.");
    }
    }
IEnumerator FailPickUp()
{

    PickUpObject();

    canMove = false;
    
    canPickUpAgain = false;

    clawController2D.StartMovingUp();


    yield return new WaitForSeconds(3);

    ReleaseObject();

    clawController2D.ResetClawPosition();
    
    Debug.Log("Finished Coroutine");
    canMove = true;
    
    canPickUpAgain = true;
    isReadyToPick = false; // Reset flag
    detectedObject = null;
    pickedObject = null;


}

IEnumerator SuccessPickUp()
{

    PickUpObject();

    canMove = false;
    
    canPickUpAgain = false;

    clawController2D.StartMovingUp();


    yield return new WaitForSeconds(3);

    prizeManager.SpawnRandomPrize();
    
    clawController2D.ResetClawPosition();

    Debug.Log("Finished Coroutine");
    canMove = true;
    isReadyToPick = false; // Reset flag
    canPickUpAgain = true;
    Destroy(detectedObject);
    Destroy(pickedObject);
    detectedObject = null;
    pickedObject = null;


}

}