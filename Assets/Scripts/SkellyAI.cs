using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyAI : CombatEntity
{
    private Transform target;
    private Animator animator;
    private Rigidbody2D rigid;

    private Vector3 direction;
    private bool activated = false;
    private float chaseRange = 15f;
    private float moveSpeed = 7f;

    private float attackRange = 3f;
    private float attackCooldown = 0f;

    private float chargeForce = 50f;
    [SerializeField] private GameObject chargeWarningPrefab;

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
        else if (Vector3.Distance(transform.position, target.transform.position) >= chaseRange)
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
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange && attackCooldown <= 0f)
        {
            StartCoroutine(Charge());
        }
    }

    private IEnumerator Charge()
    {
        attackCooldown = 3f;
        rigid.isKinematic = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        rigid.isKinematic = false;
        rigid.AddForce(direction.normalized * chargeForce, ForceMode2D.Impulse);
    }

    private void AttackTimer()
    {
        if (attackCooldown >= 0f) attackCooldown -= Time.fixedDeltaTime;
    }

    private void ChargeWarningDirection()
    {
        if (attackCooldown >= 2f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            chargeWarningPrefab.transform.rotation = rotation;
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
    void FixedUpdate()
    {
        MoveEnemy();
        Attack();
        //ChargeWarningDirection();
        AttackTimer();
        StaggerTimer();
    }
}
