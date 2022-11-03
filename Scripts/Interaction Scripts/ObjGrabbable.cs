using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How to use:
// Drop this script onto an object.
// If it has a Mesh Collider, make sure to set it to "CONVEX".
// Object must have a rigidbody component.

public class ObjGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;  // Grab Point Transform attached to Main Camera.
    [SerializeField] private float pickupForce = 150.0f;  // force applied to object as player moves and rotates
    
    // Sets object this script is placed on to correct layer, grabs the rigidbody,
    // and sets rigibody properties to desireable settings for smoothness.
    private void Awake()
    {
        gameObject.layer = 8;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Moves selected object to grab point, disables gravity and freezes rotation.
    public void Grab(Transform objectGrabPointTransform) 
    {
        //rb.constraints = RigidbodyConstraints.None;
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.drag = 10;
    }

    // Drops object held by player by nullifying the object grab point and reenabling gravity.
    public void Drop() 
    {
        objectGrabPointTransform = null;
        rb.useGravity = true;
        rb.drag = 1;
        //rb.constraints = RigidbodyConstraints.None;
    }

    // Physics objects must update in FixedUpdate.  This handles the motion of the object following the player.
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null && Vector3.Distance(transform.position, objectGrabPointTransform.position) > 0.1f) 
        {
            Vector3 moveDirection = (objectGrabPointTransform.position - transform.position);
            rb.AddForce(moveDirection * pickupForce);
        }
    }
}
