using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player : MonoBehaviour, Damagable
{
    public UIManager P_hp;

    private Rigidbody2D _rigidBody;
    private Animator _anim;
    private SpriteRenderer _sprite;
    [SerializeField]
    private float _playerSpeed = 3.0f;
    private Vector2 _playerMovement;

    public float dashSpeed = 20f;
    public float dashDuration = 0.20f;
    public float dashCooldown = 1f;
    bool isDashing = false;
    bool canDash = false;

    public int Health{get;set;}
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();

        Health = 5;
        P_hp.playerHealth = Health;

        canDash = true;
    }

    private void Update()
    {

        if (isDashing)
            return;

        //_rigidBody.velocity = _playerMovement * _playerSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        PlayerMovement();
        Attack();
    }
    
    private void PlayerMovement()
    {
        if (isDashing)
            return;

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

    public void Damage()
    {
        //Debug.Log("player got hit !");
        Health--;
        P_hp.playerHealth = Health;
        Debug.Log($"players current Health {Health}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "EnemyAttack")
            {
                Damage();
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        _rigidBody.velocity = new Vector2(_playerMovement.x * dashSpeed, _playerMovement.y* dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
