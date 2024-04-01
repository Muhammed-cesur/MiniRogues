using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    private Animator _anim;
    public float playerSpeed = 3f;
    
    InputSystem inputSystem;

    private void Start()
    {
        inputSystem = GetComponent<InputSystem>();
        controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        

        Vector3 move = new Vector3(inputSystem.HorizontalInput(), 0, inputSystem.VerticalInput());
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            _anim.SetBool("Walk", true);
            _anim.SetBool("Run", false);
        }
        if (move == Vector3.zero) 
        {
            _anim.SetBool("Walk", false);
            _anim.SetBool("Run", false);
        }

        // mobile döndürülünce burasý deðiþecek
        if (Input.GetKey(KeyCode.LeftShift ) & move != Vector3.zero) 
        {
            controller.Move(move * Time.deltaTime * playerSpeed * 2f);


            _anim.SetBool("Run", true);
            _anim.SetBool("Walk", false);
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
        playerVelocity.y += gravityValue * Time.deltaTime;
    }
}

