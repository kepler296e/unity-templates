### [FirstPersonController.cs](./Assets/Scripts/FirstPersonController.cs)
```
using UnityEngine;
using System.Collections.Generic;

public class FirstPersonController : MonoBehaviour
{
    public float walkingSpeed = 8f;
    public float runningSpeed = 12f;
    public float jumpForce = 8f;
    public float gravity = 9.8f;
    public float crouchFactor = 0.5f;
    
    private CharacterController controller;
    private Vector3 move = Vector3.zero;

    [Header("Mouse Look")]
    public float lookSpeed = 2.5f;
    public float lookLimit = 90f;
    public float zoomFactor = 2f;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Look
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(0, mouseX, 0);
        mouseY = Mathf.Clamp(mouseY, -lookLimit, lookLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);

        // Move
        float speed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        float y = move.y;
        move = transform.right * x + transform.forward * z;

        // Jump
        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            move.y = jumpForce;
        }

        // Gravity
        if (!controller.isGrounded)
        {
            move.y = y - gravity * Time.deltaTime;
        }

        controller.Move(move * Time.deltaTime);

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, crouchFactor, 1);
            controller.height *= crouchFactor;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 1, 1);
            controller.height /= crouchFactor;
        }

        // Zoom
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Camera.main.fieldOfView /= zoomFactor;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Camera.main.fieldOfView *= zoomFactor;
        }
    }
}
```
