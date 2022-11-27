using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Intefrace for Interactable objects.  Scripts using IInteractable must use all methods and set/getters here.
public interface IInteractable
{
    void Interact();
}
