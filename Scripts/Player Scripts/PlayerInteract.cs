using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script handles interactions between the player and objects in the environment.
// Objects can be either "Grabbable" or "Interactable" depending on how they are scripted.

// Interactable objects, when detected and the E key is pressed, will call their Interact() function
// as long as they inherit the IInteractable interface.

// Grabbable Objects will perform the logic of the ObjGrabbable script as long as that script is
// properly attached and set up on that object.  

public class PlayerInteract : MonoBehaviour
{
    // Reference to main camera
    [SerializeField] private Transform playerCameraTransform;

    // Distance for raycast to interact with objects
    [SerializeField] private float interactDistance;

    // Layermasks: pickup for grabbable physics objects, interact for interactable objects using IInteractables
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private LayerMask interactLayerMask;

    // UI Interaction Prompt
    [SerializeField] private GameObject crossHair;
    [SerializeField] private Vector3 chDefaultScale = new Vector3(.05f, .05f, 1);
    [SerializeField] private Color chDefaultColor = Color.green;
    [SerializeField] private Vector3 chDetectScale = new Vector3 (.09f, .09f, 1);
    [SerializeField] private Color chDetectColor = Color.yellow;
    private Image chImg;
    private Transform chTransform;

    // Grab Point attached to Main Camera
    public Transform objectGrabPointTransform;

    private void Awake()
    {
        chImg = crossHair.GetComponent<Image>();
        chTransform = crossHair.GetComponent<Transform>();
    }

    // Reference to obj we have grabbed
    private ObjGrabbable objGrabbable;

    // Anything in Update runs in real time every frame.
    private void Update()
    {
        // Display Crosshair if enabled
        InteractPrompt();

        // Grab Objects with E Key
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            // Check for Grabbed / Grabbable Objects
            GrabDrop();

            // Check for Interactable Objects
            InteractiWith();
        }
    }

    /* 
     * Uses Raycasts to check for objects on the "Grabbable" Layer.
     * If the player is not currently holding an object, moves that object
     * to a grab point attached to main camera.  If the player is holding an object,
     * the E key will drop the held object.  Grabbable objects will need to be
     * on the "Grabbable" Layer AND have the ObjGrabbable script attached.
     */
    public void GrabDrop() 
    {
        if (objGrabbable == null)
        {
            // Detects if Raycast hits a grabbable object when not already holding an object
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance, pickupLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out objGrabbable))
                {
                    objGrabbable.Grab(objectGrabPointTransform);
                    crossHair.SetActive(false);
                }
            }
        }
        else
        {
            // Drop the Object if we are already holding one when pressing "E"
            objGrabbable.Drop();
            crossHair.SetActive(true);
            objGrabbable = null;
        }
    }

    // Similar Logic to GrabDrop, but with interactable items that use IInteractable
    public void InteractiWith() 
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable)) 
            {
                interactable.Interact();
            }
        }
    }

    // Changes properties of the crosshair when user is able to interact or grab an object
    public void InteractPrompt() 
    {
        // Since we are working with 2 layers, cast 2 raycasts each detecting a separate layer.
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask)
        || Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit2, interactDistance, pickupLayerMask))
        {
            chImg.color = chDetectColor;
            chTransform.localScale = chDetectScale;
        }
        else
        {
            chImg.color = chDefaultColor;
            chTransform.localScale = chDefaultScale;
        }
    }

}
