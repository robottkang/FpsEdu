using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField]
    GameObject playerCamera;
    Vector3 move;
    Vector3 characterVelocity;
    float characterVelocityY;
    
    public float jumpForce;
    public float moveSpeed;
    public float mouseSpeed;

    float xRotate = 0;
    float yRotate = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerAim();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        move = transform.forward * Input.GetAxisRaw("Vertical") + 
               transform.right * Input.GetAxisRaw("Horizontal");

        characterVelocity = move.normalized * moveSpeed;

        if (characterController.isGrounded)
        {
            characterVelocityY = 0f;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                characterVelocityY = jumpForce;
            }
        }

        float gravityDownForce = -40f;
        characterVelocityY += gravityDownForce * Time.deltaTime;

        characterVelocity.y = characterVelocityY;

        characterController.Move(characterVelocity * Time.deltaTime);

    }

    private void PlayerAim()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        xRotate += mouseSpeed * yMouse * Time.deltaTime;
        yRotate += mouseSpeed * xMouse * Time.deltaTime;

        xRotate =  Mathf.Clamp(xRotate, -80f, 80f);

        playerCamera.transform.eulerAngles = new Vector3(-xRotate, playerCamera.transform.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(0, yRotate, 0);
    }
}
