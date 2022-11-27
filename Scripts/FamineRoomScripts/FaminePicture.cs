using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaminePicture : MonoBehaviour
{
    [SerializeField] private EventChannelSO eventChannelSO;
    [SerializeField] private AudioManagerSO audioManagerSO;
    [SerializeField] private GameObject safeDoor;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        eventChannelSO.animate += SwingOpen;
        _animator = GetComponentInParent<Animator>();
    }

    private void SwingOpen()
    {
        // Finds the 'Animator' on the current object and sets the trigger for an animation
        _animator.SetTrigger("SwingOpen");
        audioManagerSO.PlayAudio("Door Squeak", transform.position, 10f);
        // Set safe door layer to the interactable layer 
        safeDoor.layer = 10;
    }
}
