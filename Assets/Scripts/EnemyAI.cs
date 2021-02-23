using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Enemy
{
    public Rigidbody2D rigid;
    public float MoveSpeed;
    public Transform target;
    public float ChaseRange;
    public float attacRange;
    public Transform startPosition;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {

        if (Vector3.Distance(target.position, transform.position) <= ChaseRange &&
            Vector3.Distance(target.position, transform.position) > attacRange)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                rigid.isKinematic = false;
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                rigid.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }
            //else if(Vector3.Distance(target.position, transform.position) > ChaseRange)
            //{
            //    animator.SetBool("WakeUp", false);
                
            //}
        }
        else if (animator.GetBool("WakeUp"))
        {
            animator.SetBool("WakeUp", false);
            rigid.isKinematic = true;
        }
    }

    private void SetAnimatorFloats(Vector2 SetVector)
    {
        animator.SetFloat("X", SetVector.x);
        animator.SetFloat("Y", SetVector.y);
    }

    private void ChangeAnimation(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimatorFloats(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloats(Vector2.left);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimatorFloats(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimatorFloats(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newstate)
    {
        if(currentState != newstate)
        {
            currentState = newstate;
        }
    }

   
}
