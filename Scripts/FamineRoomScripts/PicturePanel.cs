using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturePanel : MonoBehaviour
{
    [SerializeField] private RotationEventChannelSO eventChannelSO;
   // List of buttons that rotate this panel
    [SerializeField] private List<string> rotateButtons;

    void Start()
    {
        // rotation event action signal binding to rotatePanel method
        eventChannelSO.rotate += rotatePanel;
    }

    void rotatePanel (string id, string direction)
        /*
         * Rotates the panel based on the button that was clicked and the 
         * direction input (left or right)
         */
    {
        if (rotateButtons.Contains(id))
        {
            switch (direction)
            {
                case "left":
                    transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
                    break;
                case "right":
                    transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                    break;
            }           
        }
    }
}
