using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = weaponHolder;
        }
        else
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon is not assigned.");
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                if (playerMovement.currentWeapon != null)
                {
                    // Overwrite weapon
                    playerMovement.currentWeapon.transform.parent = null;
                    TurnVisual(false, playerMovement.currentWeapon);
                }

                Debug.Log("Player entered trigger and picked up a new weapon");
                weapon.transform.parent = other.transform;

                // Set the specific local position for the weapon
                weapon.transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f); 

                TurnVisual(true, weapon);
                playerMovement.currentWeapon = weapon; // Set the current weapon to the new weapon
            }
        }
        else
        {
            Debug.Log("Non-player entered trigger");
        }
    }

    void TurnVisual(bool on)
    {
        foreach (var component in GetComponentsInChildren<Renderer>())
        {
            component.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        foreach (var component in weapon.GetComponentsInChildren<Renderer>())
        {
            component.enabled = on;
        }
    }
}
