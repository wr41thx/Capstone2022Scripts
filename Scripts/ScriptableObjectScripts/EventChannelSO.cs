using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scriptable object used for broadcating general events.
/// </summary>

// Creates menu entry to create new objects of this type
[CreateAssetMenu(menuName ="Scriptable Objects/Event Channel")]

public class EventChannelSO : ScriptableObject
{
    // Unity event signal to play an objects animation based on the input string as the animation trigger name
    public UnityAction  animate;
    // Event signal for returning from a ui 
    public UnityAction restoreControl;
    // Event signal to end the current  scene
    public UnityAction endScene;

    public void AnimateObject()
    {
        if (animate != null)
        {
            animate.Invoke();
        }
        else
        {
            Debug.Log(
                "An 'animate' event was requested but\n" +
                "nobody picked it up. Check that the event is being listened to."
            );
        }
    }

    public void RestorePlayerControl()
    {
        if (restoreControl != null)
        {
            restoreControl.Invoke();
        }
        else
        {
            Debug.Log(
                "A 'restoreControl' event was requested but\n" +
                "nobody picked it up. Check that the event is being listened to."
            );
        }
    }

    public void EndThisScene()
    {
        if (endScene != null)
        {
            endScene.Invoke();
        }
        else
        {
            Debug.Log(
               "An 'endScene' event was requested but\n" +
               "nobody picked it up. Check that the event is being listened to."
           );
        }
    }
}
