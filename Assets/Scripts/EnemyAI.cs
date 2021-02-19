using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Enemy
{
    //private float speed = 10f;
    public Rigidbody2D rigid;
    public float MoveSpeed;
    public Transform target;
    public float ChaseRange;
    public float attacRange;
    public Transform startPosition;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= ChaseRange &&
            Vector3.Distance(target.position, transform.position) > attacRange) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        }
    }
}
