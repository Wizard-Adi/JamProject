using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cyclopse : MonoBehaviour, Damagable
{

    public int Health { get; set; }

    private Animator _anim;

    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private Transform _spriteTransform;

    //private SpriteRenderer _currentSprite;
    //private SpriteRenderer laserSprite;

    public Transform shelter;
    public Transform player;
    private Transform _activeTarget; 

    public float maxChaseDistance = 3.0f;
    public float attackRange = 1.0f;

    public GameObject LazerObject;

    private bool _isBusy = false;
    private bool _targetReached = false;

    private void Start()
    {

        Health = 10;

        _anim = GetComponentInChildren<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject shelterObject = GameObject.FindGameObjectWithTag("Shelter");

        if (playerObj != null && shelterObject != null )
        {
            player = playerObj.transform;
            shelter = shelterObject.transform;
        }
        if (_spriteTransform == null)
        {
            _spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        }
        //_currentSprite = GetComponentInChildren<SpriteRenderer>();
        //laserSprite = LazerObject.GetComponent<SpriteRenderer>();

        _activeTarget = shelter;
    }

    private void Update()
    {
        if (_isBusy) return;

        CheckPlayer();
        MoveAndAttack();
    }

    void CheckPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < maxChaseDistance)
        {
            _activeTarget = player;
            if (transform.position.x < player.position.x)
            {

                _spriteTransform.localScale = new Vector2(-1, 1); 

                //_currentSprite.flipX = true;
                //laserSprite.flipX = true;
            }
            else
            {

                _spriteTransform.localScale = new Vector2(1, 1);

               // _currentSprite.flipX = false;
                //laserSprite.flipX = false;
            }
        }
        else
        {
            _spriteTransform.localScale = new Vector2(1, 1);


            //_currentSprite.flipX = false;
            //laserSprite.flipX = false;
            
            _activeTarget = shelter;
        }
    }

    void MoveAndAttack()
    {
        float distanceToActiveTarget = Vector2.Distance(transform.position, _activeTarget.position);

        if (distanceToActiveTarget > attackRange)
        {

            _targetReached = false;
            transform.position = Vector2.MoveTowards(transform.position, _activeTarget.position, _speed * Time.deltaTime);
            _anim.SetBool("PlayIdle", false);

            LazerObject.SetActive(false);
            _anim.SetBool("FireLazer", false);
            _anim.SetBool("AtTarget", false);
        }
        else if (!_targetReached)
        {
            _targetReached = true;
            _anim.SetBool("AtTarget", true);
            _isBusy = true;
            StartCoroutine(AttackCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        // for idle animation
        _anim.SetBool("PlayIdle", true);
        yield return new WaitForSeconds(1.0f);
        _anim.SetBool("PlayIdle", false);

        // for laser
        _anim.SetBool("FireLazer", true);
        LazerObject.SetActive(true);
        _anim.SetBool("AtTarget", true);

        yield return new WaitForSeconds(1.0f);

        LazerObject.SetActive(false);
        _anim.SetBool("FireLazer", false);
        _anim.SetTrigger("PlayIdle");

        yield return new WaitForSeconds(1.5f);

        _isBusy = false;
        _targetReached = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.tag == "PlayerAttack")
        {
            //_isBusy = true;
            Damage();
        }
    }

    public void Damage()
    {
        _anim.SetBool("PlayIdle", false);
        _anim.SetBool("FireLazer", false);
        _anim.SetBool("AtTarget", false);

        Health--;

        Debug.Log($"Cyclopse Health: {Health}");

        if (Health < 1)
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(GotHit());
    }

    IEnumerator GotHit()
    {
        _anim.SetTrigger("Hit");

        yield return new WaitForSeconds(0.4f);

        _anim.SetBool("PlayIdle", true);

        yield return new WaitForSeconds(1.0f);

        //_isBusy = false;
    }
}
