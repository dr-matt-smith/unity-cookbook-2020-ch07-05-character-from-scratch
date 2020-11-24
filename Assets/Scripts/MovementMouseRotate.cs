


using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovementMouseRotate : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    
    // gravity
    private float gravity = 9.87f;
    private float verticalSpeed = 0;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
//        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Rotate();
        
        // Move forward / backward
//        Vector3 forward = transform.TransformDirection(Vector3.forward);
//        float curSpeed = speed * Input.GetAxis("Vertical");
//        controller.SimpleMove(forward * curSpeed);
        
        Move();
    }
    
    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        Camera.main.transform.Rotate(-verticalRotation*mouseSensitivity,0,0);

        Vector3 currentRotation = Camera.main.transform.localEulerAngles;
        if (currentRotation.x > 180)
            currentRotation.x -= 360;

        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(currentRotation);
    }
    
    private void Move()
    {
        CharacterController characterController = GetComponent<CharacterController>();

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded) 
            verticalSpeed = 0;
        else 
            verticalSpeed -= gravity * Time.deltaTime;

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        
    }
}