using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    float initialMouseSensitivity;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        if (GetComponent<NetworkIdentity>().isOwned)
        {
            initialMouseSensitivity = mouseSensitivity;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (GetComponent<NetworkIdentity>().isOwned)
        {

            float mouseX = Input.GetAxis("Mouse X") * initialMouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * initialMouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
