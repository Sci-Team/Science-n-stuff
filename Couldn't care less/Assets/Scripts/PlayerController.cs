using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseY;
    private float orgMouseY;
    private float mouseX;
    private float sensMulti = 100;

    public float movementSpeed = 0.1f;
    public float mouseSensitivity = 0.1f;
    public GameObject gameCamera;
    public GameObject playerCharacter;
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;

       rigidbody = GetComponent<Rigidbody>();
       rigidbody.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        ProccessPlayerMovement();
        ProccessCameraMovement();
    }

    //uses mouse imput to rotated the camera and player model accordingly
    void ProccessCameraMovement()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * sensMulti * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity * sensMulti * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        playerCharacter.transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
        gameCamera.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0f);
    }
    //
    void ProccessPlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            rigidbody.AddRelativeForce( 0f, 0f, movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddRelativeForce(-movementSpeed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddRelativeForce(0f, 0f, -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddRelativeForce(movementSpeed, 0f, 0f);
        }
    }
}
