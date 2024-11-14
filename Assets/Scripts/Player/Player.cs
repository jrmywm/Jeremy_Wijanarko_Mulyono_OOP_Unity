using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private Animator animator;

    private float objectWidth;
    private float objectHeight;
    public Weapon currentWeapon;


    private UnityEngine.Vector2 screenBounds;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
        Instance = this; 
        } 
    }
    
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();

        screenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = 0.2f; //extents = size of width / 2
        objectHeight = 0.5f; //extents = size of height / 2
    }

        void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());

        UnityEngine.Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y+0.5f) * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;

    }
}
