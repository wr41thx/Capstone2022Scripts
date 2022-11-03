using UnityEngine;

public class SafeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Animator safeDoorAnimator;

    public void Interact()
    {
        // Opens the door and sets the door to default layer
        safeDoorAnimator.SetTrigger("OpenSafe");
        gameObject.layer = 0;
    }
}
