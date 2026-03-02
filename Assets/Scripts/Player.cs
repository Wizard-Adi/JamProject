using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _playerSpeed = 3.0f;
    private Vector2 _playerMovement;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidBody.velocity = _playerMovement * _playerSpeed;
    }

    public void Move(InputAction.CallbackContext userInput)
    {
        _playerMovement = userInput.ReadValue<Vector2>();
    }
}
