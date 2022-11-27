using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SafeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Camera> cameras= new();
    [SerializeField] private List<GameObject> controlObjects = new();
    [SerializeField] private EventChannelSO eventChannel;
    [SerializeField] private AudioManagerSO audioManagerSO;
    [SerializeField] private UIDocument safeCodeEntry;

    private Animator _animator;
    private StarterAssets.FirstPersonController _playerController;
    private SafeDialUI _safeUI;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerController = controlObjects[0].GetComponent<StarterAssets.FirstPersonController>();
        _safeUI = safeCodeEntry.GetComponent<SafeDialUI>();
        eventChannel.restoreControl += ReturnFromDialUI;
        eventChannel.endScene += RevealPrize;
    }


    public void Interact()
    {
        useMainCamera(false);
        EnableUI();
        // Disable player controller
        _playerController.ToggleController(false);
        transferControlToMouse();
        controlObjects[1].SetActive(false);
    }

    // Called when the player returns from the Safe Dail UI
    private void ReturnFromDialUI()
    {
        controlObjects[1].SetActive(true);
        useMainCamera(true);
        DisableUI();
        // Enable player controller
        _playerController.ToggleController(true);
        transferControlFromMouse();
    }

    private void transferControlToMouse()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    private void transferControlFromMouse()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void useMainCamera(bool useMain)
    {
        cameras[0].enabled = useMain;
        cameras[0].GetComponent<AudioListener>().enabled = useMain;

        cameras[1].enabled = !useMain;
        cameras[1].GetComponent<AudioListener>().enabled = !useMain;

    }

    private void RevealPrize()
    {
        ReturnFromDialUI();
        controlObjects[3].SetActive(true);
        StartCoroutine(TriggerSafeDoor());

        // Activate the scene door so the player can leave the room
        controlObjects[2].SetActive(true);
        controlObjects[4].SetActive(true);
    }

    private void EnableUI()
    {
        // Enables the UI document in the camera view
        safeCodeEntry.enabled = true;
        // Enables the UI document's script
       _safeUI.enabled = true;
    }

    private void DisableUI()
    {
        // Disables the UI document in the camera view
        safeCodeEntry.enabled = false;
        // Disables the UI document's script
        _safeUI.enabled = false;
    }

    IEnumerator<WaitForSeconds> TriggerSafeDoor()
    {
        yield return new WaitForSeconds(0.3f);
        audioManagerSO.PlayAudio("Open Safe", transform.position);
        _animator.SetTrigger("OpenSafe");
    }
}
