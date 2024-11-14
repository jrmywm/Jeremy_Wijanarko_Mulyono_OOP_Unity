using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    private InvincibilityComponent invincibility;
    private void Start()
    {
        invincibility = GetComponent<InvincibilityComponent>();
    }
    public void Damage(int damage)
    {
        if (healthComponent != null)
        {
            if(invincibility == null || !invincibility.isInvincible)
            {
                healthComponent.Subtract(damage);
            }
        }
    }

    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            if(invincibility == null || !invincibility.isInvincible)
            {
                healthComponent.Subtract(bullet.damage);
            }
        }
    }
}
