using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    private Vector2 newPosition;

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            if (player.currentWeapon == null)
            {
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.instance.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    }
}
