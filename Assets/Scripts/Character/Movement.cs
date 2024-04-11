using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float gravityValue = -3.81f;

    private Animator _anim;
    public float playerSpeed;

    [SerializeField] private float _Currenthealth;
    [SerializeField] private float _Maxhealth;

     [SerializeField] private HealthBarSystem HealthBar;

    InputSystem inputSystem;

    private void Start()
    {
        inputSystem = GetComponent<InputSystem>();
        controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            PlayerTakeDamage(5);
        }

        Vector3 move = new Vector3(inputSystem.HorizontalInput(), 0, inputSystem.VerticalInput());
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            //_anim.SetBool("Walk", true);
            _anim.SetBool("Run", true); // false normalde
        }
        if (move == Vector3.zero) 
        {
           // _anim.SetBool("Walk", false);
            _anim.SetBool("Run", false);
        }

        // mobile döndürülünce burasý deðiþecek
        if (Input.GetKey(KeyCode.LeftShift ) & move != Vector3.zero) 
        {
            controller.Move(move * Time.deltaTime * playerSpeed * 2f);


            _anim.SetBool("Run", true);
            //_anim.SetBool("Walk", false);
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
        playerVelocity.y += gravityValue * Time.deltaTime;


    }
    public void PlayerTakeDamage(float damage)
    {
        _Currenthealth -= damage;
        _anim.SetTrigger("Hit");
        HealthBar.UpdateHealthBar(_Currenthealth, _Maxhealth);

        if (_Currenthealth <= 0)
        {

            _anim.SetTrigger("Die");

            Invoke(nameof(DestroyPlayer), 3f);
        }


    }
    void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}

