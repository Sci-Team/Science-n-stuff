using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseY;
    private float orgMouseY;
    private float mouseX;
    private float currentMaxSpeed;
    private float multiplierValue = 100f;
    private float moveHorizontal;
    private float moveVertical;
    private Boolean inAir;

    private GameObject playerCharacter;

    public float baseJumpHeight = 500f;
    public float baseMovementSpeed = 5;
    public float mouseSensitivity = 0.1f;
    public GameObject gameCamera;
    public Collider objectCollider;
    public Rigidbody playerRigidbody;


    Vector3 moveDirection;
    Vector3 cameraPos;
    Vector3 jumpDirection;


    // Start is called before the first frame update
    //########################################################
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        moveDirection = new Vector3(0f, 0f, 0f);
        jumpDirection = new Vector3(0f, baseJumpHeight * multiplierValue , 0f);

        playerCharacter = this.gameObject;
        playerRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();

        playerRigidbody.rotation = Quaternion.identity;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        currentMaxSpeed = baseMovementSpeed * multiplierValue;
        inAir = true;
    }

    // Update is called once per frame
    //########################################################
    void Update()
    {
        ProccessCameraMovement();

        if (inAir){ ProccessPlayerMovement(); }
        

        //jumping
        if (Input.GetKeyDown("space") && inAir) { playerRigidbody.AddRelativeForce(jumpDirection); }
    }

    
    void FixedUpdate()
    {
        playerRigidbody.AddRelativeForce(moveDirection * currentMaxSpeed);
        gameCamera.transform.position = transform.position + new Vector3 (0f, 2f, 0f);
    }



    //uses mouse imput to rotated the camera and player model accordingly
    //########################################################
    private void ProccessCameraMovement()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * multiplierValue * Time.deltaTime;
        mouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * multiplierValue * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        playerCharacter.transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
        gameCamera.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);
    }

    //Controls players movement in game world
    //########################################################
    private void ProccessPlayerMovement()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();

    }

    
    //Prevents movement while in air
    private void OnTriggerExit(Collider other)
    {
        inAir = false;
    }

    //allows movement while on the ground
    private void OnTriggerEnter(Collider other)
    {
        currentMaxSpeed = baseMovementSpeed * multiplierValue;
        inAir = true;
    }
    
}


