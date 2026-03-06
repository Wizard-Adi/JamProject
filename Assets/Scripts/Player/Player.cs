using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _anim;
    private SpriteRenderer _sprite;
    [SerializeField]
    private float _playerSpeed = 3.0f;
    private Vector2 _playerMovement;

    

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        //_rigidBody.velocity = _playerMovement * _playerSpeed;

        PlayerMovement();
        Attack();
    }
    
    private void PlayerMovement()
    {
        _rigidBody.velocity = _playerMovement * _playerSpeed;

        if (_playerMovement.x > 0)
        {
            _sprite.flipX = false;
        }
        else if (_playerMovement.x < 0)
        {
            _sprite.flipX = true;
        }
    }

    public void Move(InputAction.CallbackContext userInput)
    {
        _anim.SetBool("Walk", true);

        if (userInput.canceled)
        {
            _anim.SetBool("Walk", false);
        }

        _playerMovement = userInput.ReadValue<Vector2>();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("Attack");
        }
    }
}
