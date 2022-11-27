using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Invokes a Unity Event when the item with the desired tag
// collides with it.  Is attached to Trigger object of Pedestal.

public class Pedastal : MonoBehaviour
{
    [SerializeField] private UnityEvent pedestalEvent;
    [SerializeField] private string desiredItemTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == desiredItemTag) 
        {
            pedestalEvent.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
