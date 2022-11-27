using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerFloor : MonoBehaviour
{
    [SerializeField] private float lowerRate;
    [SerializeField] private float descendTime;
    [SerializeField] private GameObject[] ragDolls;

    [SerializeField] private AudioClip liftStopClip;
    [SerializeField] private GameObject descendAudio;

    [SerializeField] private GameObject player;

    private float targetY;
    private bool canLower;
    
    // Ragdolls do not play nicely with the floor lowering
    // Made them a child of the floor, so they descend properly, but disable
    // them during descent, and reenable after floor has stopped moving.

    private void Awake()
    {
        canLower = false;
        ragDolls = GameObject.FindGameObjectsWithTag("RagDollGrabPoint");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (canLower) 
        {
            Descend();
        }
    }

    public void triggerFloor() 
    {
        foreach (GameObject doll in ragDolls)
        {
            doll.layer = 0;
        }

        canLower = true;
        descendAudio.GetComponent<AudioSource>().Play();
        player.transform.parent = gameObject.transform;
        StartCoroutine(DescendTimer(descendTime));
    }

    private void Descend() 
    {
        targetY = transform.position.y;
        targetY -= lowerRate * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    private IEnumerator DescendTimer(float descendTime) 
    {
        yield return new WaitForSeconds(descendTime);
        descendAudio.GetComponent<AudioSource>().Stop();
        descendAudio.GetComponent<AudioSource>().PlayOneShot(liftStopClip);
        canLower = false;
        foreach (GameObject doll in ragDolls)
        {
            doll.layer = 8;
        }
    }
}
