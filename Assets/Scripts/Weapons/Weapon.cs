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

    void Awake()
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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

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

    private void FixedUpdate()
    {
        if (Time.time > timer && objectPool != null && Player.Instance.hasWeapon == true)
        {
            Bullet bulletObject = objectPool.Get();
            if (bulletObject != null)
            {
                bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                timer = Time.time + shootIntervalInSeconds;
            }
        }
    }
}