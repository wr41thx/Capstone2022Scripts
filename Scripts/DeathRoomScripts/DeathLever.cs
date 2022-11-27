using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DeathLever : MonoBehaviour, IInteractable
{
    private Animator leverAnim;

    [SerializeField] private AudioClip leverSound;
    [SerializeField] private UnityEvent leverEvent;

    private void Awake()
    {
        leverAnim = gameObject.GetComponent<Animator>();
    }


    private void TriggerLever() 
    {
        AudioSource.PlayClipAtPoint(leverSound, transform.position);
        leverAnim.Play("LeverCorrect", 0, 0f);
        gameObject.layer = 0;
        leverEvent.Invoke();
    }

    public void Interact() 
    {
        TriggerLever();
    }

}
