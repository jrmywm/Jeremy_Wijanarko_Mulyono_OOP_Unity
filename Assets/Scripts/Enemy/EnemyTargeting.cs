using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 0.5f;
    private Vector2 screenBounds;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    void SpawnEnemy()
    {
        float spawnY = Random.Range(-screenBounds.y, screenBounds.y);
        if (Random.value > 0.5f)
        {
            // Spawn from left
            transform.position = new Vector2(-screenBounds.x, spawnY);
        }
        else
        {
            // Spawn from right
            transform.position = new Vector2(screenBounds.x, spawnY);
        }
    }

    void MoveEnemy()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}