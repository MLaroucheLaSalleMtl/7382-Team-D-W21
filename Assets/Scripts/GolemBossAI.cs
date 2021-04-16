using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;
    private Rigidbody2D rigid;

    private Vector3 direction;
    private float chaseRange = 3f;
    private float moveSpeed = 2f;

    private Vector3 skillDirection;
    private float attackCooltime = 4f;

    [SerializeField] private GameObject earthShieldPrefab;
    private int earthShieldCount = 5;
    private float earthShieldCooldown = 0f;

    [SerializeField] private GameObject fallingRockPrefab;
    [SerializeField] private GameObject roomAnchor;
    [SerializeField] private CameraShake camShake;
    private int randomFallingRockCount = 20;
    private int trailingFallingRockCount = 10;

    [SerializeField] private GameObject bossLock;
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text text;
    [SerializeField] private FinalDoor finalDoor;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private PlayerMovement player;

    private void FacePlayer()
    {
        direction = target.position - transform.position;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    private void MoveBoss()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > chaseRange && attackCooltime <= 3f)
        {
            animator.SetBool("Walking", true);
            rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private IEnumerator EarthShield()
    {
        attackCooltime = 5f;
        earthShieldCooldown = 20f;
        animator.SetTrigger("EarthShield");
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < earthShieldCount; i++)
        {
            GameObject shield = Instantiate(earthShieldPrefab, transform.position, Quaternion.identity);
            Orbit script = shield.GetComponent<Orbit>();
            script.Axis = gameObject.transform;
            script.Angle = (360f / earthShieldCount) * i;
        }
    }

    private IEnumerator GroundSlam()
    {
        attackCooltime = 6f;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.7f);
        //camShake.enabled = !camShake.enabled;
        //camShake.ShakingDuration = 1.5f;
        yield return new WaitForSeconds(1.3f);
        int attack = Random.Range(1, 3);
        switch (attack)
        {
            case 1:
                RandomFallingRocks();
                break;
            case 2:
                StartCoroutine(TrailingFallingRocks());
                attackCooltime += 2f;
                break;
        }
    }

    private void RandomFallingRocks()
    {
        Instantiate(fallingRockPrefab, target.transform.position, Quaternion.identity);
        for(int i = 0; i < randomFallingRockCount - 1; i++)
        {
            float x = Random.Range(-8f, 8f);
            float y = Random.Range(-8f, 8f);
            Vector3 location = new Vector3(roomAnchor.transform.position.x + x, roomAnchor.transform.position.y + y, 0f);
            Instantiate(fallingRockPrefab, location, Quaternion.identity);
        }
    }

    private IEnumerator TrailingFallingRocks()
    {
        for (int i = 0; i < trailingFallingRockCount; i++)
        {
            Instantiate(fallingRockPrefab, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public override void Die()
    {
        if (IsDead())
        {
            if (!HasDied)
            {
                HasDied = true;
                animator.SetTrigger("Die");
                CancelInvoke();
                StopAllCoroutines();
                StartCoroutine(SkillLevelUp());
                finalDoor.DeadBoss++;

                //Marco
                healthBar.SetActive(false);
                player.ChangeMusic = true;
            }
        }
    }

    private IEnumerator SkillLevelUp()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().EarthShieldCount = 5;
        textBox.SetActive(true);
        text.text = "YOUR SKILL EARTH SHIELD HAS BEEN POWERED UP!" +
            "\nIT WILL NOW CREATE 5 ROCKS!";
        yield return new WaitForSeconds(4f);
        textBox.SetActive(false);
        bossLock.SetActive(false);
        Destroy(gameObject);
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
        if (!IsDead())
        {
            MoveBoss();
            attackCooltime -= Time.fixedDeltaTime;
            earthShieldCooldown -= Time.fixedDeltaTime;
            if (earthShieldCooldown <= 0f && attackCooltime <= 0f)
            {
                StartCoroutine(EarthShield());
            }
            if (attackCooltime <= 0f && Vector3.Distance(transform.position, target.transform.position) < chaseRange)
            {
                StartCoroutine(GroundSlam());
            }
        }
        Die();
    }
}
