using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(gameObject.tag))
        {
            return;
        }

        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
        InvincibilityComponent invincibility = collision.GetComponent<InvincibilityComponent>();

        if (hitbox != null)
        {
            Debug.Log(bulletPrefab);
            if (bulletPrefab != null)
            {
                hitbox.Damage(bulletPrefab);
            }
            else
            {
                hitbox.Damage(damage);
            }

            
            if (invincibility != null && invincibility.isInvincible == false)
            {
                invincibility.StartBlinking();
            }
        }
    }
}

