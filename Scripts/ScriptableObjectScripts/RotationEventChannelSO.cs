using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///  Sends an event cue to a listener.
/// </summary>

// Creates context menu entry to access and create this scriptable object
[CreateAssetMenu(menuName = "Scriptable Objects/Rotation Event Channel")]

public class RotationEventChannelSO : ScriptableObject
{
   // Events that can be invoked/listened to by subscribers to this object
    public UnityAction<string, string> rotate;
    public UnityAction updatePosition;

    public void RaiseEvent(string callerID, string direction)
    {
        // Checks for an event listener
        if (rotate != null)
        {
            // Invokes the rotation event feeding it the id of it's caller and the rotation direction
            rotate.Invoke(callerID, direction);
        }
        else
        {
            Debug.LogWarning(
                "A 'rotate' event was request but nobody picked it up\n" +
                "Check that the event is being listened to"
            );
        }
    }

    // Sends event signal to notify the subscriber that an objects position (or rotation) have changed
    public void ChangePostion()
    {
        // Check for an event listener
        if (updatePosition != null)
        {
            updatePosition.Invoke();
        }
        else
        {
            Debug.LogWarning(
                "An 'updatePosition' event was request but nobody picked it up\n" +
                "Check that the event is being listened to"
            );
        }
    }
}
