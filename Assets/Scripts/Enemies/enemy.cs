using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Animator _anim;

    public float xAxis = 9.09f;
    public float yAxis;
    [SerializeField]private float _speed = 3.0f;

    public Vector2 targetPosition;
    public Vector2 tempTargetPosition;
    private bool _targetReached = false;

    public GameObject LazerObject;

    public Transform player;
    public float maxChaseDistance = 10.0f;


    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();

        yAxis = Random.Range(-4.71f, 4.18f);
        targetPosition = new Vector2(0, yAxis);
        tempTargetPosition = targetPosition;

        var position = new Vector2(xAxis, yAxis);
        //Instantiate(enemyObject, position, Quaternion.identity);
    }

    private void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        //transform.Translate(Vector2.left * _speed * Time.deltaTime)

        if ( Vector2.Distance(transform.position, player.transform.position) < maxChaseDistance)
        {
            targetPosition = player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
        else
        {
            targetPosition = tempTargetPosition;
        }

        if (_targetReached != true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 1.0f)
            {
                _targetReached = true;
                _anim.SetBool("FireLazer", true);
                LazerObject.SetActive(true);
                _anim.SetBool("AtTarget", true);
            }
        }
        else if (_targetReached == true && Vector2.Distance(transform.position, targetPosition) > 1.0f)
        {
            _targetReached = false;
            LazerObject.SetActive (false);
            _anim.SetBool("FireLazer", false);
            _anim.SetBool("AtTarget", false);
        }
    }
}
