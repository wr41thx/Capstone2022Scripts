using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTimer;
    private void Update()
    {
        Destroy(this.gameObject, destroyTimer);  // to limit bullet instances in game scene
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
