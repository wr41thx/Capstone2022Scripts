using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] GameObject crossHair;
    [SerializeField] AudioManagerSO audioManagerSO;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndSceneItem")
        {
            other.gameObject.SetActive(false);
            audioManagerSO.PlayAudio("Evil Laugh", transform.position);
            crossHair.SetActive(true);
        }
    }
}
