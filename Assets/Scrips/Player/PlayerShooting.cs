using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
           //Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

  /*  void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bc = bullet.GetComponent<BulletController>();
        bc.SetDirection(firePoint.right); // ºŸ…ËΩ«…´≥Ø”“
    }*/
}