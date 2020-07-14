using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    float y = 0.0f;
    float verticalVelocity = 0;
    float horizontalSpeed = 2.0f;
    float verticalSpeed = 2.0f;
    float backAndForth = 5.0f;
    float sideToSide = 5.0f;


    void Start()
    {
       
    }


    void FixedUpdate()
    {
        //Rotate player based on mouse movement
        float rotation = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, 5.0f * rotation, 0);
        float updown = -verticalSpeed * Input.GetAxis("Mouse Y");

        //clamp allowed rotation to 30
        if (y + updown > 50 || y + updown < 0)
        {
            updown = 0;
        }

        y += updown;

        //Player movement
        float forwardSpeed = backAndForth * Input.GetAxis("Vertical");
        float lateralSpeed = sideToSide * Input.GetAxis("Horizontal");
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        //Rotate camera according to where player is currently facing
        Camera.main.transform.RotateAround(transform.position, transform.right, updown);

        CharacterController characterController = GetComponent<CharacterController>();
        Vector3 speed = new Vector3(lateralSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        //Move player
        characterController.Move(speed * Time.deltaTime);

        
    }


}
