//using System.Collections;
//using System.Collections.Generic;
////using Unity.Mathematics;
//using UnityEngine;

//public class enemy : MonoBehaviour
//{
//    private Animator _anim;

//    public float xAxis = 9.09f;
//    public float yAxis;
//    [SerializeField]private float _speed = 1.5f;

//    public Vector2 targetPosition;
//    public Vector2 tempTargetPosition;
//    private bool _targetReached = false;

//    public GameObject LazerObject;

//    public Transform player;
//    public float maxChaseDistance = 1.5f;

//    private bool _isBusy = false;


//    private void Start()
//    {
//        _anim = GetComponentInChildren<Animator>();

//        yAxis = Random.Range(-4.71f, 4.18f);
//        targetPosition = new Vector2(0, yAxis);
//        tempTargetPosition = targetPosition;

//        var position = new Vector2(xAxis, yAxis);
//        //Instantiate(enemyObject, position, Quaternion.identity);
//    }

//    private void Update()
//    {
//        if (_isBusy == true)
//            return;
//        EnemyMovement();
//    }

//    void EnemyMovement()
//    {
//        //transform.Translate(Vector2.left * _speed * Time.deltaTime)

//        if ( Vector2.Distance(transform.position, player.transform.position) < maxChaseDistance)
//        {
//            targetPosition = player.transform.position;
//            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
//        }
//        else
//        {
//            targetPosition = tempTargetPosition;
//        }

//        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

//        if (distanceToTarget > 1.0f)
//        {
//            _targetReached = false;

//            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
//            _anim.SetBool("AtTarget", false);

//            LazerObject.SetActive(false);
//            _anim.SetBool("FireLazer", false);
//            _anim.SetBool("AtTarget", false);

//        }

//        else if(!_targetReached)
//        {
//            _targetReached = true;
//            _isBusy = true;
//            StartCoroutine(Cooldown());
//        }
//    }

//    //IEnumerator Cooldown()
//    //{
//    //    Debug.Log("Inside Coroutine");
//    //    _anim.SetTrigger("PlayIdle");
//    //    yield return new WaitForSeconds(1.5f);
//    //    _anim.SetBool("FireLazer", true);
//    //    LazerObject.SetActive(true);
//    //    _anim.SetBool("AtTarget", true);

//    //    _isBusy = false;
//    //}

//    IEnumerator Cooldown()
//    {
//        Debug.Log("Starting Attack Cycle");

//        _anim.SetTrigger("PlayIdle");
//        yield return new WaitForSeconds(1.5f);

//        _anim.SetBool("FireLazer", true);
//        LazerObject.SetActive(true);
//        _anim.SetBool("AtTarget", true);

//        yield return new WaitForSeconds(2.0f);

//        LazerObject.SetActive(false);
//        _anim.SetBool("FireLazer", false);
//        _anim.SetBool("AtTarget", false);

//        yield return new WaitForSeconds(1.0f);

//        _isBusy = false;
//        _targetReached = false;
//    }
//}
