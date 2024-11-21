using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    void Awake()
    {
        weapon = weapon.GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.LogError("Weapon Holder is not assigned in the Inspector.");
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false, weapon);
        }
        else
        {
            Debug.LogError("Weapon is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            if (player.currentWeapon != null)
            {
                // Overwrite weapon
                player.currentWeapon.transform.parent = null;
                TurnVisual(false, player.currentWeapon);
            }

            if (weapon != null)
            {
                Debug.Log("Player entered trigger and picked up a new weapon");
                weapon.transform.parent = other.transform;

                // Set the specific local position for the weapon
                weapon.transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);

                TurnVisual(true, weapon);
                player.currentWeapon = weapon; // Set the current weapon to the new weapon
                player.hasWeapon = true; // Update player's weapon status
            }
            else
            {
                Debug.LogError("Weapon is not assigned.");
            }
        }
        else
        {
            Debug.Log("Non-player entered trigger");
        }
    }

    private void TurnVisual(bool on)
    {
        foreach (var component in GetComponentsInChildren<Renderer>())
        {
            component.enabled = on;
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        foreach (var component in weapon.GetComponentsInChildren<Renderer>())
        {
            component.enabled = on;
        }
    }
}
