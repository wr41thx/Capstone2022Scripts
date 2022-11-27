using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{

    // Logic for planes in mini game

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private Transform bombSpawnPoint;
    [SerializeField] private float bombRate;
    [SerializeField] private float minDropRate;
    [SerializeField] private float maxDropRate;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float speed;
    private bool canBomb;

    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip explosionSound;

    private void Awake()
    {
        gameAudio = GameObject.FindGameObjectWithTag("CCAudio").GetComponent<AudioSource>();
        canBomb = false;
        StartCoroutine(CoolDown());
        speed = Random.Range(minSpeed, maxSpeed);  // each plane has a random speed

        // Planes that spawn on the right side need to move left
        // on the X axis.

        if (gameObject.tag == "RightBomber") 
        {
            speed = -speed;
        }
    }

    private void Update()
    {
        MovePlane();
        if (canBomb) 
        {
            bombRate = Random.Range(minDropRate, maxDropRate);
            SpawnBomb();
        }
        
    }

    private void MovePlane() 
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void SpawnBomb() 
    {
        canBomb = false;
        GameObject missile = Instantiate(bombPrefab, bombSpawnPoint.position, bombSpawnPoint.rotation);
        StartCoroutine(CoolDown());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroys planes that have left the screen.
        if (other.gameObject.tag == "Despawn") 
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Bullet")
        {
            // Play animation, delay, then destroy
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            PlayExplosionSound();
            Destroy(this.gameObject);
            Destroy(explosion, 1f);
        }
    }

    private void PlayExplosionSound() 
    {
        gameAudio.PlayOneShot(explosionSound);
    }

    // Controls the intervals in which planes drop bombs.
    IEnumerator CoolDown() 
    {
        yield return new WaitForSeconds(bombRate);
        canBomb = true;
    }
}
