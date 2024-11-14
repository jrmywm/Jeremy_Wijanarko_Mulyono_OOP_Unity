using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;
    private Vector2 screenBounds;
    private bool movingRight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SpawnEnemy();
    }

    void Update()
    {
        MoveEnemy();
    }

    void SpawnEnemy()
    {
        float spawnY = Random.Range(-screenBounds.y+1, screenBounds.y);
        if (Random.value > 0.5f)
        {
            transform.position = new Vector2(-screenBounds.x, spawnY);
            movingRight = true;
        }
        else
        {
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
}
