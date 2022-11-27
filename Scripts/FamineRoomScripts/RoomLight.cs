using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLight : MonoBehaviour
{
    [SerializeField] EventChannelSO eventChannelSO;

    // Start is called before the first frame update
    void Start()
    {
        eventChannelSO.endScene += TurnOff;
    }

    // Turn off the light when the scene ends
    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
