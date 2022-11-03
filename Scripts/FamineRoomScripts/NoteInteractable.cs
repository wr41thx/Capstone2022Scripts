using UnityEngine;
using UnityEngine.UIElements;

public class NoteInteractable : MonoBehaviour,  IInteractable
{
   [SerializeField] private UIDocument note;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) & note.enabled)
        {
            //Lefte click hides the note
            note.enabled = false;
        }
    }

    public void Interact()
    {
        //Display note to player
        note.enabled = true;
    }
}
