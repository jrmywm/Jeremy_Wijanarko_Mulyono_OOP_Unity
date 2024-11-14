using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float speed = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private Vector2 screenBounds;
    private bool movingRight;
    private float nextFireTime;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SpawnEnemy();
    }


    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        Shoot();
    }

    void SpawnEnemy()
    {
        float spawnY = Random.Range(-screenBounds.y, screenBounds.y);
        if (Random.value > 0.5f)
        {
            // Spawn from left
            transform.position = new Vector2(-screenBounds.x, spawnY);
            movingRight = true;
        }
        else
        {
            // Spawn from right
            transform.position = new Vector2(screenBounds.x, spawnY);
            movingRight = false;
        }
    }

    void MoveEnemy()
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
