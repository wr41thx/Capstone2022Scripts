using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    // Basic logic for bombs dropped by planes.  
    
    [SerializeField] private GameObject bombExplosionFX;
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip explosionSound;

    private void Awake()
    {
        gameAudio = GameObject.FindGameObjectWithTag("CCAudio").GetComponent<AudioSource>();
    }

    private void PlayExplosionSound() 
    {
        gameAudio.PlayOneShot(explosionSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet") 
        {
            GameObject explosion = Instantiate(bombExplosionFX, transform.position, transform.rotation);
            PlayExplosionSound();
            Destroy(this.gameObject);
            Destroy(explosion, 1f);
        }

        if (other.tag == "KillZone") 
        {
            Destroy(this.gameObject, .2f);
        }
    }
}
