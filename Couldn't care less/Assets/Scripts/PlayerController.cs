using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseY;
    private float orgMouseY;
    private float mouseX;
    private float currentSpeed;
    private float sensMulti = 100;
    private Boolean keyPress;

    private Rigidbody rigidbody;
    private GameObject playerCharacter;

    public float baseMovementSpeed = 0.1f;
    public float mouseSensitivity = 0.1f;
    public GameObject gameCamera;
    public Collider objectCollider;

    Vector3 moveDirection = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    //########################################################
    void Start()
    {
        playerCharacter = this.gameObject;
    
        Cursor.lockState = CursorLockMode.Locked;

        rigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();

        rigidbody.rotation = Quaternion.identity;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
        if (keyPress == true)
        {
            moveDirection = new Vector3(0f, 0f, 0f);
            keyPress = false;
        }
        else
        {
            rigidbody.AddRelativeForce(moveDirection);
            keyPress = true;
        }
    }



    //uses mouse imput to rotated the camera and player model accordingly
    //########################################################
    void ProccessCameraMovement()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * sensMulti * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity * sensMulti * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        playerCharacter.transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
        gameCamera.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);
    }

    //Controls players movement in game world
    //########################################################
    void ProccessPlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            moveDirection.z = currentSpeed;
            keyPress = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -currentSpeed;
            keyPress = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -currentSpeed;
            keyPress = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = currentSpeed;
            keyPress = false;
        }
        
    }

    //Prevents movement while in air
    private void OnTriggerExit(Collider other)
    {
        currentSpeed = 0;
    }

    //allows movement while on the ground
    private void OnTriggerEnter(Collider other)
    {
        currentSpeed = baseMovementSpeed;
    }
    
}
