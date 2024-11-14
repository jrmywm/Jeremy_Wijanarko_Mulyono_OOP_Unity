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

    public void SetPool(IObjectPool<Bullet> pool)
    {
        this.pool = pool;
    }

    void Awake()
    {
        // Initialization code if needed
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.up * bulletSpeed;
        }
        else    
        {
            Debug.LogError("Rigidbody2D component not found on the GameObject.");
        }
    }

    void Update()
    {
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
        // Implementasikan logika untuk menangani collision dengan objek lain
        // Misalnya, mengurangi health objek yang terkena peluru
        // Setelah itu, kembalikan peluru ke pool
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Debug.LogError("Pool is not set. Please ensure SetPool is called before OnTriggerEnter2D.");
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
