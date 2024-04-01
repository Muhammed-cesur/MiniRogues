using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private Player _player;
    private Vector2 _Moveposition;

    private void Awake()
    {
        _player = new Player();
    }

    private void OnEnable()
    {
        _player.Enable();
        _player.PlayerMove.PlayerMovement.performed += OnplayerMovement; 
    }

    private void OnDisable()
    {
        _player?.Disable();
        _player.PlayerMove.PlayerMovement.performed -= OnplayerMovement;

    }
    private void OnplayerMovement (InputAction.CallbackContext context)
    {
        OnMove();
    }
    private void OnMove()
    {
       _Moveposition = _player.PlayerMove.PlayerMovement.ReadValue<Vector2>();
    }
    private void Update()
    {
        OnMove();
    }

    public float HorizontalInput()
    {
        return _Moveposition.x;
    }
    public float VerticalInput()
    {
        return _Moveposition.y;
    }

}
