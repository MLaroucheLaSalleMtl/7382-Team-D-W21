using System.Collections;
using UnityEngine;

public class FireBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;

    private Vector3 direction;
    private Vector3 skillDirection;
    private float attackCooltime = 5f;

    [SerializeField] private GameObject projectilePrefab;
    private float fireballSpeed = 12f;

    [SerializeField] private GameObject[] flameWallPrefab;
    private int flameWallIndex;


    private void FacePlayer()
    {
        direction = target.position - transform.position;
        //animator.SetFloat("X", direction.x);
        //animator.SetFloat("Y", direction.y);
    }

    private void Fireball()
    {
        attackCooltime = 5f;
        GameObject fireball = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
        rigid.AddForce(direction.normalized * fireballSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator FlameWall()
    {
        attackCooltime = 10f;
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
            yield return new WaitForSeconds(1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("FacePlayer", 0f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attackCooltime -= Time.fixedDeltaTime;
        if (attackCooltime <= 0f)
        {
            int attack = Random.Range(1, 3);
            switch (attack)
            {
                case 1:
                    Fireball();
                    break;
                case 2:
                    StartCoroutine(FlameWall());
                    break;
            }
        }
    }
}
