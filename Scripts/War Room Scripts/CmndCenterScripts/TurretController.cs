using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // Controls for the mini game turret.

    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool canControl;
    [SerializeField] private float maxZRot;
    [SerializeField] private float minZRot;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;
    [SerializeField] private bool canShoot;

    [SerializeField] private AudioSource gameAudioSource;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        canShoot = true;
    }

    private void Update()
    {
        // Control is relinquished when player presses E whlie playing.
        // See the mini game manager and display controller for how this works.

        if (canControl) 
        {
            RotateTurret();
            FireTurret();
        }
    }

    private void RotateTurret() 
    {
        // Rotate Left
        if (Input.GetKey(KeyCode.A))
        {  
            if (transform.rotation.eulerAngles.z <= maxZRot) 
            {
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }           
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.D))
        {  
            if (transform.rotation.eulerAngles.z >= minZRot) 
            {
                transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
            }          
        }
    }

    private void FireTurret() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot) 
        {
            canShoot = false;
            SpawnBullet();
            PlayShootSound();
            StartCoroutine(CoolDown());
        }
    }

    private void PlayShootSound() 
    {
        gameAudioSource.PlayOneShot(shootSound);
    }

    private void SpawnBullet() 
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);
    }

    // Enables / Disables turrt controller from Manager
    public void toggleTurret(bool toggle) 
    {
        canControl = toggle;
    }

    // For fire rate of turret
    IEnumerator CoolDown() 
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
