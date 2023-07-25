using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Rigidbody2D gunRb;
    Vector2 mousePos;

    [SerializeField]
    private float bulletForce = 20f;
    [SerializeField]
    private float shootingDelay = 0.5f; // Delay between shots
    [SerializeField]
    private float continuousBulletDelay = 0.2f; // Delay between continuous bullets
    [SerializeField]
    private int maxContinuousBullets = 3; // Maximum number of continuous bullets the player can shoot at a time
    private int bulletsFired;

    [SerializeField]
    private AudioSource playerShoot;

    private bool canShoot = true;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
        }
    }

    // Sets a delay before allowing continuous shooting
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootingDelay);
        canShoot = true;
        bulletsFired = 0;
    }

    IEnumerator ContinuousBulletDelay()
    {
        yield return new WaitForSeconds(continuousBulletDelay);
        canShoot = true;
    }

    void Shoot()
    {
        if (bulletsFired >= maxContinuousBullets) // Check if the maximum number of continuous bullets has been reached
        {
            canShoot = false; 
            StartCoroutine(ShootDelay()); // Start a delay before allowing continuous shooting again
            return; 
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); 

        playerShoot.Play();

        bulletsFired++; 

        if (bulletsFired < maxContinuousBullets) // Check if there are more bullets to be fired in the continuous sequence
        {
            canShoot = false; 
            StartCoroutine(ContinuousBulletDelay()); // Start a delay before firing the next continuous bullet
        }
    }
}
