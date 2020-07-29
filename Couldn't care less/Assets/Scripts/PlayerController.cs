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
    private float currentSpeed;
    private float sensMulti = 100f;
    private float moveMulti = 100f;
    private float moveHorizontal;
    private float moveVertical;

    //private Boolean inAir = false;

    private Rigidbody playerRigidbody; 
    private GameObject playerCharacter;

    public float jumpHeight = 500f;
    public float baseMovementSpeed = 5;
    public float mouseSensitivity = 0.1f;
    public GameObject gameCamera;
    public Collider objectCollider;
    
  

    Vector3 moveDirection;
    Vector3 cameraPos;
    

    // Start is called before the first frame update
    //########################################################
    void Start()
    {
        moveDirection = new Vector3(0f, 0f, 0f);
           
        Cursor.lockState = CursorLockMode.Locked;

        playerCharacter = this.gameObject;
        playerRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();

        playerRigidbody.rotation = Quaternion.identity;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    //########################################################
    void Update()
    {
        ProccessCameraMovement();
        ProccessPlayerMovement();
        
    }

    
    void FixedUpdate()
    {
        playerRigidbody.AddRelativeForce(moveDirection * currentSpeed);
        gameCamera.transform.position = transform.position + new Vector3 (0f, 2f, 0f);
    }



    //uses mouse imput to rotated the camera and player model accordingly
    //########################################################
    private void ProccessCameraMovement()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * sensMulti * Time.deltaTime;
        mouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * sensMulti * Time.deltaTime;

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
        currentSpeed = 0f;
        moveDirection.y = 0f;
        //inAir = true;
    }

    //allows movement while on the ground
    private void OnTriggerEnter(Collider other)
    {
        currentSpeed = baseMovementSpeed * moveMulti;
        //inAir = false;
    }
    
}


