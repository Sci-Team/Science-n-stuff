using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mouseY;
    public float mouseX;
    public float mouseSensitivity = 100f;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 fp;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        ProcessInput();
    }

    void ProcessInput()
    {
        camera = new Vector3(mouseX, mouseY, 0f);
        transform.Rotate(fp);
    }
}
