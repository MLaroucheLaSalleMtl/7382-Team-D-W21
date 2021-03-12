using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;

    private Vector3 direction;
    private Vector3 skillDirection;
    private float attackCooltime = 2f;

    [SerializeField] private GameObject projectilePrefab;
    private int fanBarrage = 5;
    private int fanCount = 5;
    private float fanSpread = 20f;
    private int barrageCount = 30;
    private int attackCounter = 3;


    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void LeafFan()
    {
        attackCooltime = 5f;
        StartCoroutine(Fan());
    }

    private IEnumerator Fan()
    {
        for (int i = 0; i < fanBarrage; i++)
        {
            skillDirection = Quaternion.AngleAxis(-(fanSpread * ((float)fanCount - 1f) / 2), Vector3.forward) * direction;
            for (int j = 0; j < fanCount; j++)
            {
                GameObject missile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
                rigid.AddForce(skillDirection.normalized * 8f, ForceMode2D.Impulse);
                skillDirection = Quaternion.AngleAxis(fanSpread, Vector3.forward) * skillDirection;
            }
            yield return new WaitForSeconds(0.75f);
        }
        attackCounter--;
    }

    private void LeafBarrage()
    {
        attackCooltime = 5f;
        StartCoroutine(Barrage());
    }

    private IEnumerator Barrage()
    {
        for (int i = 0; i < barrageCount; i++)
        {
            GameObject missile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
            rigid.AddForce(direction.normalized * 12f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
        attackCounter--;
    }

    private IEnumerator BossSleep()
    {
        animator.SetBool("Sleeping", true);
        attackCounter = 3;
        yield return new WaitForSeconds(5f);
        animator.SetBool("Sleeping", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("FacePlayer", 0f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!animator.GetBool("Sleeping")) attackCooltime -= Time.fixedDeltaTime;
        if (attackCooltime <= 0f)
        {
            int attack = Random.Range(1, 3);
            switch(attack)
            {
                case 1:
                    LeafFan();
                    break;
                case 2:
                    LeafBarrage();
                    break;
            }
        }
        if (attackCounter == 0) StartCoroutine(BossSleep());
    }
}
