using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    private BoxCollider2D bc;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2* maxSpeed / timeToFullSpeed;
        moveFriction = (-2) * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2) * maxSpeed / (timeToStop * timeToStop);;
        
    }

    public void Move()
    {
        
        // set value 0 setiap update
        moveDirection = Vector2.zero;

        // Input menggunakan secara manual W A S D karena input manager entah kenapa ada lag nya
        if (Input.GetKey(KeyCode.W)) moveDirection.y = 1;
        if (Input.GetKey(KeyCode.S)) moveDirection.y = -1;

        if (Input.GetKey(KeyCode.A)) moveDirection.x = -1;
        if (Input.GetKey(KeyCode.D)) moveDirection.x = 1;

        Vector2 targetVelocity = moveDirection * moveVelocity;

        // Jika ada input, tingkatkan kecepatan menuju targetVelocity
        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, GetFriction().magnitude * Time.fixedDeltaTime);

            // Batasi kecepatan maksimum sesuai dengan maxSpeed
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y));
        }
        else
        {
            // Pengurangan kecepatan dengan stopFriction
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, GetFriction().magnitude * Time.fixedDeltaTime * 0.5f);
            
            // Batas kecepatan rendah
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x && Mathf.Abs(rb.velocity.y) < stopClamp.y)
            {
                // Set kecepatan menjadi nol
                rb.velocity = Vector2.zero; 
            }
        }
    }
    
    public Vector2 GetFriction() 
    {
        if(moveDirection == Vector2.zero)
        {
            return stopFriction;
        }
        else
        {
            return moveFriction;
        }
    }

    public void MoveBound()
    {
        // masih dikosongkan sesuai dengan soal
    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
        {
        }


}