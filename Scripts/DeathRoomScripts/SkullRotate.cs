using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRotate : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float step;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;
    [SerializeField] private float rotSpeed;

    [SerializeField] private bool canRotate;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (canRotate) 
        {
            step = Time.deltaTime * rotSpeed;
            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position) 
                * Quaternion.Euler(offsetX, offsetY, offsetZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);
        }
    }

    public void EnableRotateSkull() 
    {
        canRotate = true;
    }

}
