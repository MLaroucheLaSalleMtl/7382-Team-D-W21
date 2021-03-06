using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRangeAI : CombatEntity
{
    public Rigidbody2D rigid;
    public float MoveSpeed = 3f;
    public Transform target;
    public float ChaseRange = 10f;
    public float attacRange = 6f;
    public Transform startPosition;
    private Animator animator;

    private Vector3 direction;
    private float attackCooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = State.idle;
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();

        StaggerTimer();
        AttackTimer();
    }

    void CheckDistance()
    {
        SetDirection();
        if (Vector3.Distance(target.position, transform.position) <= ChaseRange &&
            Vector3.Distance(target.position, transform.position) > attacRange)
        {
            animator.SetBool("WakeUp", true);

            if (CurrentState == State.idle || CurrentState == State.walk && CurrentState != State.stagger && attackCooldown <= 0f)
            {
                rigid.MovePosition(direction);
                ChangeState(State.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= attacRange)
        {
            if (CurrentState == State.idle || CurrentState == State.walk && CurrentState != State.stagger && CurrentState != State.attack)
            {
                if (attackCooldown <= 0f) StartCoroutine(RangeAttack());
            }
        }
        else if (animator.GetBool("WakeUp"))
        {
            animator.SetBool("WakeUp", false);
            ChangeState(State.idle);
            attackCooldown = 1f;
        }
    }

    private void SetAnimatorFloats(Vector2 SetVector)
    {
        animator.SetFloat("X", SetVector.x);
        animator.SetFloat("Y", SetVector.y);
    }

    private void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimatorFloats(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimatorFloats(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
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

    private void ChangeState(State newstate)
    {
        if (CurrentState != newstate)
        {
            CurrentState = newstate;
        }
    }

    private void SetDirection()
    {
        direction = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        ChangeAnimation(direction - transform.position);
    }

    private IEnumerator RangeAttack()
    {
        attackCooldown = 2f;
        ChangeState(State.attack);
        animator.SetBool("Attacking", true);
        Vector2 direction = target.position - transform.position;
        GameObject missile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
        rigid.AddForce(direction.normalized * 8f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        ChangeState(State.idle);
        animator.SetBool("Attacking", false);
    }

    private void AttackTimer()
    {
        if (attackCooldown >= 0f && animator.GetBool("WakeUp")) attackCooldown -= Time.fixedDeltaTime;
    }
}
