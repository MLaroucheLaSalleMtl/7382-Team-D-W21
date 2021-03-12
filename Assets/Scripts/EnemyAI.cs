using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CombatEntity
{
    public Rigidbody2D rigid;
    public float MoveSpeed;
    public Transform target;
    public float ChaseRange;
    public float attacRange;
    public Transform startPosition;
    private Animator animator;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.idle;
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        StaggerTimer();
    }

    void CheckDistance()
    {
        SetDirection();
        if (Vector3.Distance(target.position, transform.position) <= ChaseRange &&
            Vector3.Distance(target.position, transform.position) > attacRange)
        {
            if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
            {
                rigid.MovePosition(direction);
                ChangeState(State.walk);
                animator.SetBool("WakeUp", true);
                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= attacRange)
        {
            if (currentState == State.idle || currentState == State.walk && currentState != State.stagger && currentState != State.attack)
            {
                StartCoroutine(Attack());
            }
        }
        else if (animator.GetBool("WakeUp"))
        {
            animator.SetBool("WakeUp", false);
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

    private void SetDirection()
    {
        direction = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        ChangeAnimation(direction - transform.position);
    }

    private IEnumerator Attack()
    {
        ChangeState(State.attack);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        ChangeState(State.idle);
    }

    private void ChangeState(State newstate)
    {
        if(currentState != newstate)
        {
            currentState = newstate;
        }
    }
}
