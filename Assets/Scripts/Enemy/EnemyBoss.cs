using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fireRate = 1f;
    private bool movingRight = true;
    private Vector2 screenBounds;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // Ensure this is assigned in the inspector

    private float nextFireTime;


    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x > screenBounds.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < -screenBounds.x)
            {
                movingRight = true;
            }
        }
    }

    void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.down * bullet.GetComponent<Bullet>().bulletSpeed; // Assuming bullets are shot downwards by EnemyBoss
            }
            nextFireTime = Time.time + fireRate;
        }
    }
}
