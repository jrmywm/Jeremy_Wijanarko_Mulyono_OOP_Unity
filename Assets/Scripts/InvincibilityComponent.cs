using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7; // Number of times the object blinks
    [SerializeField] private float blinkInterval = 0.1f; // Interval between blinks
    [SerializeField] private Material blinkMaterial; // Material to use during blinking

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial; 
    private Coroutine blinkRoutine; 
    public bool isInvincible = false;
    private bool isPlayer = false;

    void Start()
    {
        isPlayer = gameObject.CompareTag("Player"); // Check if the object is tagged as "Player"

        if (isPlayer)
        {
            // If the object is the player, find the "Ship" child and get its SpriteRenderer
            Transform shipTransform = transform.Find("Ship");
            if (shipTransform != null)
            {
                spriteRenderer = shipTransform.GetComponent<SpriteRenderer>();
            }
        }
        else
        {
            // If the object is not the player, get the SpriteRenderer of the current object
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material; // Store the original material
        }
        else
        {
            Debug.LogError("SpriteRenderer not found!");
        }
    }

    private IEnumerator BlinkingRoutine()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.material = blinkMaterial; 
                yield return new WaitForSeconds(blinkInterval); 
                spriteRenderer.material = originalMaterial; 
                yield return new WaitForSeconds(blinkInterval); 
            }
        }

        isInvincible = false; 
    }

    public void StartBlinking()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
        }
        blinkRoutine = StartCoroutine(BlinkingRoutine());
    }
}