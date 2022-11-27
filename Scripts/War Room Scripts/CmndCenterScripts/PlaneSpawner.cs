using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    // Controls the two empty plane spawning objects.
    // Planes can spawn at randomized intervals on both sides.
    // This is controlled via two coroutines.

    [SerializeField] private Transform leftPlaneSpawner;
    [SerializeField] private Transform rightPlaneSpawner;

    [SerializeField] private GameObject leftPlanePrefab;
    [SerializeField] private GameObject rightPlanePrefab;
    
    private float leftSpawnTimer;
    private float rightSpawnTimer;

    [SerializeField] private float minTimeLeft;
    [SerializeField] private float minTimeRight;
    [SerializeField] private float maxTimeLeft;
    [SerializeField] private float maxTimeRight;

    [SerializeField] private bool canSpawnLeft;
    [SerializeField] private bool canSpawnRight;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        canSpawnLeft = true;
        canSpawnRight = true;
    }
    private void OnEnable()
    {
        canSpawnLeft = true;
        canSpawnRight = true;
    }

    private void Update()
    {
        if (canSpawnLeft) 
        {
            canSpawnLeft = false;
            leftSpawnTimer = Random.Range(minTimeLeft, maxTimeLeft);
            SpawnPlane(leftPlaneSpawner, leftPlanePrefab);
            StartCoroutine(CoolDownLeft(leftSpawnTimer));
        }

        if (canSpawnRight) 
        {
            canSpawnRight = false;
            rightSpawnTimer = Random.Range(minTimeRight, maxTimeRight);
            SpawnPlane(rightPlaneSpawner, rightPlanePrefab);
            StartCoroutine(CoolDownRight(rightSpawnTimer));
        }
    }

    private GameObject SpawnPlane(Transform planeSpawner, GameObject plane) 
    {
        return Instantiate(plane, planeSpawner.position, planeSpawner.rotation);
    }

    public void EnableSpawns(bool enable) 
    {
        canSpawnLeft = enable;
        canSpawnRight = enable;
    }

    IEnumerator CoolDownLeft(float spawnTimer) 
    {
        yield return new WaitForSeconds(spawnTimer);
        canSpawnLeft = true;
    }
    IEnumerator CoolDownRight(float spawnTimer)
    {
        yield return new WaitForSeconds(spawnTimer);
        canSpawnRight = true;
    }


}
