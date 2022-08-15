using UnityEngine;
using System.Collections.Generic;

public class FPSController : MonoBehaviour
{
    private float walkingSpeed = 8f;
    private float runningSpeed = 12f;
    private float jumpSpeed = 10f;
    private float zoomFOV = 50;
    private CharacterController cc;
    private Vector3 moveDirection;
    private Camera mainCamera;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded) moveDirection.y = jumpSpeed;
        else moveDirection.y = movementDirectionY;
        if (!cc.isGrounded) moveDirection.y -= 9.81f * Time.deltaTime;

        if (Input.GetKey(KeyCode.C)) cc.height = 1f;
        else cc.height = 2f;
        mainCamera.transform.localPosition = new Vector3(0, cc.height / 2, 0);

        if (Input.GetKey(KeyCode.V)) mainCamera.fieldOfView = zoomFOV;
        else mainCamera.fieldOfView = 90;

        cc.Move(moveDirection * Time.deltaTime);
    }
}