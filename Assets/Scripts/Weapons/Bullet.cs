using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool;

    public void Deactivate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        if (pool != null)
        {
            pool.Release(this);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        rb.velocity = Vector2.up * bulletSpeed;
        if (Camera.main != null)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        }
        else
        {
            Debug.LogError("Main Camera not found. Please ensure there is a Camera tagged as 'MainCamera' in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }
        Deactivate();   
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}