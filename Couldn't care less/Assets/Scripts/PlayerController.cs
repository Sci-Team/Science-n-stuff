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
    private float sensMulti = 100f;
    private float moveMulti = 100f;
    
    private Boolean inAir;
    private Boolean keyPress;

    private Rigidbody rigidbody;
    private GameObject playerCharacter;

    public float jumpHeight = 500f;
    public float baseMovementSpeed = 0.1f;
    public float mouseSensitivity = 0.1f;
    public GameObject gameCamera;
    public Collider objectCollider;

    Vector3 jump = new Vector3(0f, 0f, 0f);
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
        rigidbody.AddRelativeForce(moveDirection);
        rigidbody.AddForce(jump * jumpHeight, ForceMode.Impulse);
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
        //Movement on Z axis
        if (Input.GetKey(KeyCode.W))
        { 
            moveDirection.z = currentSpeed;
           
        }else if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -currentSpeed;
        }
        else { moveDirection.z = 0f; }

        //Movement on X axis
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = currentSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -currentSpeed;
        }
        else { moveDirection.x = 0f; }

        //Movement on Y axis
        if (Input.GetKey(KeyCode.Space) && inAir == false){
            moveDirection.y = jumpHeight;
            inAir = false;
        }
        
    }

    //Prevents movement while in air
    private void OnTriggerExit(Collider other)
    {
        currentSpeed = 0f;
        moveDirection.y = 0f;
        inAir = true;
    }

    //allows movement while on the ground
    private void OnTriggerEnter(Collider other)
    {
        currentSpeed = baseMovementSpeed * moveMulti;
        
    }
    
}
