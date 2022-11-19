# Simple [Player.cs](./Scripts/Player.cs)
## Mouse Look and Movement
```
// Mouse Look
float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
mouseY -= Input.GetAxis("Mouse Y") * lookSpeed;
transform.Rotate(0, mouseX, 0);
mouseY = Mathf.Clamp(mouseY, -lookLimit, lookLimit);
Camera.main.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);

// Movement
float speed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;
float x = Input.GetAxis("Horizontal") * speed;
float z = Input.GetAxis("Vertical") * speed;
float y = move.y;
move = transform.right * x + transform.forward * z;
```
## Jump
```
if (Input.GetKey(KeyCode.Space) && cc.isGrounded)
{
    move.y = jumpForce;
}

// Gravity
if (!cc.isGrounded)
{
    move.y = y - gravity * Time.deltaTime;
}
```
## Crouch
```
if (Input.GetKeyDown(KeyCode.LeftControl))
{
    transform.localScale = new Vector3(1, crouchFactor, 1);
    cc.height *= crouchFactor;
}
else if (Input.GetKeyUp(KeyCode.LeftControl))
{
    transform.localScale = new Vector3(1, 1, 1);
    cc.height /= crouchFactor;
}
```
## Zoom
```
if (Input.GetKeyDown(KeyCode.LeftAlt))
{
    Camera.main.fieldOfView /= zoomFactor;
}
else if (Input.GetKeyUp(KeyCode.LeftAlt))
{
    Camera.main.fieldOfView *= zoomFactor;
}
```
