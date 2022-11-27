using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBody : MonoBehaviour
{
    [SerializeField] EventChannelSO eventChannelSO;

    void OnTriggerExit(Collider other)
    {
        eventChannelSO.EndThisScene(); 
    }
}
