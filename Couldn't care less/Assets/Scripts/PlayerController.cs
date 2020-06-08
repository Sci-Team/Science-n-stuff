using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseY;
    private float mouseX;

    public float mouseSensitivity = 100f;
    public GameObject gameCamera;
    public GameObject playerCharacter;
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
       rigidbody = GetComponent<Rigidbody>();

       rigidbody.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        ProccessCameraMovement();
    }

    void ProccessCameraMovement()
    {
        playerCharacter.transform.Rotate(0f, mouseX, 0f, Space.World);
        gameCamera.transform.Rotate(-mouseY, 0f, 0f, Space.Self);
    }
}
