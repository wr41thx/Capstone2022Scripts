using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjectInJar : MonoBehaviour
{
    [SerializeField] GlassJar jarOnPedestal;

    void OnTriggerEnter(Collider other)
    {
        jarOnPedestal.Set_hasCorrectObject(true);
    }
}
