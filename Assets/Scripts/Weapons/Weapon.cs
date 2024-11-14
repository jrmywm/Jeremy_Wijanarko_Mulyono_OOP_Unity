using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;
    private Player player; // Reference to PlayerMovement

    void Awake()
    {
        player = FindObjectOfType<Player>(); // Find PlayerMovement in the scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    void Update()
    {
        if (player.currentWeapon == null) return; // Do not shoot if player hasn't picked up the weapon

        if (Time.time > timer)
        {
            Shoot();
            timer = Time.time + shootIntervalInSeconds;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        return newBullet;
    }

    private void OnGet(Bullet bullet)
    {
        if (bullet == null)
        {
            Debug.LogError("Bullet is null!");
        }
        else
        {
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Shoot()
    {
        Bullet newBullet = objectPool.Get();
        if (newBullet == null)

        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }
}