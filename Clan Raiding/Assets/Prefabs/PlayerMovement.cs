using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprit = 12f;
    public float jumpHight = 2f;
    public float gravity = -30;
    [Space]
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public float mouseSensitivity = 300f;
    float initialMouseSensitivity;

    public Transform playerBody;
    public Transform playerHolderCam;
    public GameObject playerCam;

    float xRotation = 0f;

    Vector3 velocity;
    public bool isGrounded;

    void Start()
    {
        if (GetComponent<NetworkIdentity>().isOwned)
        {
            initialMouseSensitivity = mouseSensitivity;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NetworkIdentity>().isOwned)
        {
            playerCam.SetActive(true);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -10;
            }

            float X = Input.GetAxis("Horizontal");
            float Z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * X + transform.forward * Z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
            }

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                controller.Move(move * speed * sprit * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X") * initialMouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * initialMouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerHolderCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}