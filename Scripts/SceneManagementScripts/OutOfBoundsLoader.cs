using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutOfBoundsLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (sceneToLoad == "TutorialScene") 
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }   
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
