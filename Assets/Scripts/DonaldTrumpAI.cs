using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DonaldTrumpAI : CombatEntity
{
    private Transform target;
    private Animator animator;
    private Rigidbody2D rigid;

    private Vector3 direction;
    private float chaseRange = 1.5f;
    private float moveSpeed = 2f;

    private bool activated = false;
    private float attackCooltime = 0f;

    [SerializeField] private Image blackScreen;

    public bool Activated { get => activated; set => activated = value; }

    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void MoveBoss()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > chaseRange && attackCooltime <= 0f)
        {
            animator.SetBool("Moving", true);
            rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void Attack()
    {
        if (attackCooltime <= 0f && Vector3.Distance(transform.position, target.transform.position) < chaseRange)
        {
            animator.SetBool("Attacking", true);
            attackCooltime = 2f;
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    public override void Die()
    {
        if(IsDead())
        {
            if (!HasDied)
            {
                HasDied = true;
                animator.SetTrigger("Die");
                rigid.velocity = Vector3.zero;
                rigid.isKinematic = true;
            }

            blackScreen.gameObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a + 0.3f * Time.deltaTime);
            if(blackScreen.color.a >= 1f)
            {
                SceneManager.LoadScene("EndGameScreen");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (!IsDead())
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
                FacePlayer();
                MoveBoss();
                attackCooltime -= Time.fixedDeltaTime;
                Attack();
            }
            Die();
        }
    }
}
