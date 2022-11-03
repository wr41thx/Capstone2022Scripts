using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private RotationEventChannelSO eventChannelSO;
    [SerializeField] private string callerID;
    [SerializeField] private string direction;
    
    public void Interact()
    {
        // Raises event to rotate a game object
        eventChannelSO.RaiseEvent(callerID, direction);
        // Raises event to notify a subscriber that an object has been rotated
        eventChannelSO.ChangePostion();
    }
}
