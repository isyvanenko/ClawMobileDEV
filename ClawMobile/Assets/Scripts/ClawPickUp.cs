using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPickUp : MonoBehaviour
{
   public Transform attachmentPoint; // Reference to the attachment point on the claw
    private GameObject pickedObject;  // Object currently picked up by the claw

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is pickable (use a tag or layer to identify pickable objects)
        if (other.CompareTag("Pickable") && pickedObject == null)
        {
            // Attach the object to the claw
            pickedObject = other.gameObject;
            pickedObject.transform.SetParent(attachmentPoint); // Make the object a child of the attachment point
            pickedObject.transform.localPosition = Vector3.zero; // Snap the object to the attachment point
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = true; // Disable physics on the picked object
        }
    }

    void Update()
    {
        // Release the picked object when pressing a key (optional)
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
}