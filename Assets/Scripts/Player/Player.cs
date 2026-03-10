using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, Damagable
{
    public GGscript gg;
    public GGscript gg2;
    public Minotaur Minotaur;
    public cyclopse Cyclopse;

    public int PlayerAttackPower;

    public int PlayerNeutralDamage = 0;

    public UIManager P_hp;
    public SpawnManager P_dead;
    public PauseMenu deadinfo;

    private Rigidbody2D _rigidBody;
    private Animator _anim;
    private SpriteRenderer _sprite;

    [SerializeField]
    private float _playerSpeed = 5.0f;
    private Vector2 _playerMovement;

    public float dashSpeed = 20f;
    public float dashDuration = 0.20f;
    public float dashCooldown = 0.5f;

    bool isDashing = false;
    bool canDash = false;

    private AudioSource _audioSource;

    public AudioClip slashClip;
    public AudioClip dashClip;
    public AudioClip damageClip;

    private int _enemyDamage;

    public int Health{get;set;}
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        Health = 50;
        P_hp.playerHealth = Health;

        PlayerNeutralDamage = 8;

        PlayerAttackPower = PlayerNeutralDamage;

        //gg.PlayerAttackPower = PlayerNeutralDamage;
        //gg2.PlayerAttackPower = PlayerNeutralDamage;
        //Minotaur.PlayerAttackPower = PlayerNeutralDamage;
        //Cyclopse.PlayerAttackPower = PlayerNeutralDamage;

        P_hp.PlayerMoodStore = "Neutral";

        canDash = true;
    }

    private void Update()
    {
        _ChangeMood += Time.deltaTime;

        if (_ChangeMood >= 30.0f)
        {
            _ChangeMood = 0;
            PlayerMood();
        }

        BoundaryCheck();

        if (isDashing)
            return;

        //_rigidBody.velocity = _playerMovement * _playerSpeed;

        //if (_ChangeMood >= 15.0f)
        //{
        //    _ChangeMood = 0;
        //    PlayerMood();
        //}

        
        if ((Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.Space)) && canDash))
        {
            StartCoroutine(Dash());
        }

        PlayerMovement();
        Attack();
    }

    void BoundaryCheck()
    {
        if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector2(-9.3f, transform.position.y);
        }
        else if (transform.position.x >= 15.2f)
        {
            transform.position = new Vector2(15f, transform.position.y);
        }
        
        if (transform.position.y <= -7.1f)
        {
            transform.position = new Vector2(transform.position.x, -7f);
        }
        else if (transform.position.y >= 9.2f)
        {
            transform.position = new Vector2(transform.position.x, 9f);
        }
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

            StartCoroutine(SlashEffect());

            //PlaySFX(slashClip);
        }
    }

    IEnumerator SlashEffect()
    {
        //PlaySFX(slashClip);
        yield return new WaitForSeconds(0.4f);
        PlaySFX(slashClip);
    }

    public void Damage()
    {
        //Debug.Log("player got hit !");
        _enemyDamage = Random.Range(5, 8);

        PlaySFX(damageClip);

        Health -= _enemyDamage;
        P_hp.playerHealth = Health;

        //P_dead._stopSpawning = true;
        if (Health <=0)
        {
            P_dead.OnPlayerDeath();

            deadinfo.PlayerDead = true;

            Destroy(gameObject);
            //Application.Quit();

            //SceneManager.LoadScene("MainMenu");
        }

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

        //dash clip effect
        PlaySFX(dashClip);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private int _playerMood;
    private int _moodDuration;
    private float _ChangeMood;

    void PlayerMood()
    {
        _playerMood = Random.Range(0, 4);

        switch (_playerMood)
        {
            case 0:
                Debug.Log("Neutral");
                // Dashing speed  = 15.0f; , Dashing cooldown = 0.6f, Attack Damage ?

                P_hp.PlayerMoodStore = "Neutral";


                dashSpeed = 15f;
                dashDuration = 0.20f;
                dashCooldown = 0.6f;

                PlayerAttackPower = PlayerNeutralDamage;

                //gg.PlayerAttackPower = PlayerNeutralDamage;
                //gg2.PlayerAttackPower = PlayerNeutralDamage;
                //Minotaur.PlayerAttackPower = PlayerNeutralDamage;
                //Cyclopse.PlayerAttackPower = PlayerNeutralDamage;

                break;
            case 1:
                Debug.Log("Sad");
                // Dashing speed  = 10.0f; , Dashing cooldown = 0.75f, Attack Damage ?

                P_hp.PlayerMoodStore = "Sad";


                dashSpeed = 10f;
                dashDuration = 0.20f;
                dashCooldown = 0.75f;

                PlayerAttackPower = PlayerNeutralDamage - 4;


                //gg.PlayerAttackPower = PlayerNeutralDamage - 4;
                //gg2.PlayerAttackPower = PlayerNeutralDamage - 4;
                //Minotaur.PlayerAttackPower = PlayerNeutralDamage - 4;
                //Cyclopse.PlayerAttackPower = PlayerNeutralDamage - 4;

                break;
            case 2:
                Debug.Log("Happy");
                // Dashing speed  = 20.0f; , Dashing cooldown = 0.5f, Attack Damage ?

                P_hp.PlayerMoodStore = "Happy";


                dashSpeed = 20f;
                dashDuration = 0.20f;
                dashCooldown = 0.5f;

                PlayerAttackPower = PlayerNeutralDamage + 7;


                //gg.PlayerAttackPower = PlayerNeutralDamage + 7;
                //gg2.PlayerAttackPower = PlayerNeutralDamage + 7;
                //Minotaur.PlayerAttackPower = PlayerNeutralDamage + 7;
                //Cyclopse.PlayerAttackPower = PlayerNeutralDamage + 7;

                break;
            case 3:
                Debug.Log("Raged");
                // Dashing speed  = 28.0f; , Dashing cooldown = 0.3f, Attack Damage ?

                P_hp.PlayerMoodStore = "Raged";


                dashSpeed = 28f;
                dashDuration = 0.20f;
                dashCooldown = 0.3f;

                PlayerAttackPower = PlayerNeutralDamage + 12;


                //gg.PlayerAttackPower = PlayerNeutralDamage + 12;
                //gg2.PlayerAttackPower = PlayerNeutralDamage + 12;
                //Minotaur.PlayerAttackPower = PlayerNeutralDamage + 12;
                //Cyclopse.PlayerAttackPower = PlayerNeutralDamage + 12;

                break;
        }
    }

    void PlaySFX(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
