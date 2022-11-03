using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triggers the Panel on the side of the generator to 
// fall and make a noise when in contact with screwdriver
public class PanelTrigger : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] AudioSource panelAudio;

    private void Awake()
    {
        panelAudio = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Screw Driver")
        {
            panelAudio.Play();
            Rigidbody rb = panel.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            panel.AddComponent<ObjGrabbable>();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
