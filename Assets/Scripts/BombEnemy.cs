using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : CombatEntity
{
    private Transform target;
    private Animator animator;
    private Rigidbody2D rigid;

    private Vector3 direction;
    private bool activated = false;
    private float chaseRange = 15f;
    private float moveSpeed = 3f;

    private float attackRange = 2f;
    private float attackCooldown = 0f;
    [SerializeField] private GameObject explosionPrefab;

    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void MoveEnemy()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < chaseRange && !activated)
        {
            activated = true;
        }
        else if(Vector3.Distance(transform.position, target.transform.position) >= chaseRange)
        {
            activated = false;
        }

        if (activated && CurrentState == State.idle && attackCooldown <= 0f)
        {
            animator.SetBool("Walking", true);
            rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void Attack()
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= attackRange && attackCooldown <= 0f)
        {
            attackCooldown = 1f;
            rigid.velocity = Vector3.zero;
            rigid.isKinematic = true;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 2f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("FacePlayer", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        Attack();
        StaggerTimer();
    }
}
