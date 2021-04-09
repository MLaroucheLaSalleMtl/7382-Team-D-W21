using System.Collections;
using UnityEngine;

public class FireBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;
    private Rigidbody2D rigid;

    private Vector3 direction;
    private float chaseRange = 5f;
    private float moveSpeed = 2f;

    private Vector3 skillDirection;
    private float attackCooltime = 2f;

    [SerializeField] private GameObject projectilePrefab;
    private float fireballSpeed = 15f;

    [SerializeField] private GameObject[] flameWallPrefab;
    private int flameWallIndex;

    private int counter = 0;
    [SerializeField] private GameObject counterPrefab;


    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void MoveBoss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > chaseRange && attackCooltime <= 3f)
        {
            animator.SetBool("Walking", true);
            rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void Fireball()
    {
        attackCooltime = 4f;
        GameObject fireball = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
        rigid.AddForce(direction.normalized * fireballSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator FlameWall()
    {
        attackCooltime = 6f;
        flameWallIndex = Random.Range(0, 3);
        for(int i = 0; i < flameWallPrefab.Length; i++)
        {
            if (flameWallIndex >= flameWallPrefab.Length)
            {
                flameWallIndex = 0;
            }
            BossSummonFlameWall flameWall = flameWallPrefab[flameWallIndex].GetComponent<BossSummonFlameWall>();
            flameWall.Activated = true;
            flameWallIndex += 1;
            yield return new WaitForSeconds(0.66f);
        }
    }

    public override void ReceiveDamage(float damage)
    {
        if (!Invincible)
        {
            Hp -= damage;
            counter++;
            Invincible = true;
            if (IsDead())
            {
                Die();
            }
            StartCoroutine("DamageFeedback");
            StartCoroutine("InvincibilityTimer");
        }
    }

    private void Counter()
    {
        if(counter >= 10)
        {
            counter = 0;
            attackCooltime = 6f;
            Instantiate(counterPrefab, transform.position, Quaternion.identity);
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
        MoveBoss();
        Counter();
        attackCooltime -= Time.fixedDeltaTime;
        if (attackCooltime <= 0f)
        {
            int attack = Random.Range(1, 4);
            switch (attack)
            {
                case 1:
                    Fireball();
                    break;
                case 2:
                    StartCoroutine(FlameWall());
                    break;
                case 3:
                    Fireball();
                    break;
            }
        }
    }
}
