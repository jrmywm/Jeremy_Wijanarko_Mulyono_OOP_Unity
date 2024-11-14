using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 2f;
    private Vector2 screenBounds;
    private Vector2 direction;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log("Screen Bounds: " + screenBounds);
        SpawnEnemy();
    }

    void Update()
    {
        MoveEnemy();
    }

    void SpawnEnemy()
    {
        float spawnX = Random.Range(-screenBounds.x, screenBounds.x);
        transform.position = new Vector2(spawnX, screenBounds.y);
        direction = Vector2.down; 
    }

    void MoveEnemy()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Debug.Log("Enemy Position: " + transform.position);
    }
}