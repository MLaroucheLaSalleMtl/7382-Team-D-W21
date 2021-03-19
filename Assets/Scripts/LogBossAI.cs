using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;

    private Vector3 direction;
    private Vector3 skillDirection;
    private float attackCooltime = 5f;
    private int attackCounter = 3;

    [SerializeField] private GameObject leafPrefab;
    private int fanBarrage = 3;
    private int fanBarrageCount = 3;
    private int fanCount = 5;
    private float fanSpread = 30f;

    private int barrageCount = 30;

    [SerializeField] private GameObject whirlwindPrefab;
    private int whirlwindCount = 3;
    private int whirlwindLeafCount = 10;


    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private IEnumerator LeafFan()
    {
        attackCooltime = 5f;
        for (int i = 0; i < fanBarrage; i++)
        {
            for (int j = 0; j < fanBarrageCount; j++)
            {
                skillDirection = Quaternion.AngleAxis(-(fanSpread * ((float)fanCount - 1f) / 2), Vector3.forward) * direction;
                for (int k = 0; k < fanCount; k++)
                {
                    GameObject missile = Instantiate(leafPrefab, transform.position, Quaternion.identity);
                    Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
                    rigid.AddForce(skillDirection.normalized * 8f, ForceMode2D.Impulse);
                    skillDirection = Quaternion.AngleAxis(fanSpread, Vector3.forward) * skillDirection;
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.75f);
        }
        attackCounter--;
    }

    private IEnumerator LeafBarrage()
    {
        attackCooltime = 5f;
        for (int i = 0; i < barrageCount; i++)
        {
            GameObject missile = Instantiate(leafPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rigid = missile.GetComponent<Rigidbody2D>();
            rigid.AddForce(direction.normalized * 12f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
        attackCounter--;
    }

    private IEnumerator LeafWhirlwind()
    {
        attackCooltime = 8f;
        for (int i = 0; i < whirlwindCount; i++)
        {
            for(int j = 0; j < whirlwindLeafCount; j++)
            {
                GameObject leaf = Instantiate(whirlwindPrefab, transform.position, Quaternion.identity);
                Orbit script = leaf.GetComponent<Orbit>();
                script.Angle = 360f * 0.1f * j;
            }
            yield return new WaitForSeconds(1.5f);
        }
        attackCounter--;
    }

    private IEnumerator BossSleep()
    {
        animator.SetBool("Sleeping", true);
        attackCounter = 3;
        yield return new WaitForSeconds(5f);
        animator.SetBool("Sleeping", false);
        attackCooltime = 5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Sleeping", false);
        target = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("FacePlayer", 0f, 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!animator.GetBool("Sleeping")) attackCooltime -= Time.fixedDeltaTime;
        if (attackCooltime <= 0f)
        {
            int attack = Random.Range(3, 4);
            switch(attack)
            {
                case 1:
                    StartCoroutine(LeafFan());
                    break;
                case 2:
                    StartCoroutine(LeafBarrage());
                    break;
                case 3:
                    StartCoroutine(LeafWhirlwind());
                    break;
            }
        }
        if (attackCounter == 0) StartCoroutine(BossSleep());
    }
}
