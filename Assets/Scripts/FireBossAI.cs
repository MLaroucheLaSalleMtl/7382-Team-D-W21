using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossAI : CombatEntity
{
    private Transform target;
    private Animator animator;

    private Vector3 direction;
    private Vector3 skillDirection;
    private float attackCooltime = 5f;

    [SerializeField] private GameObject projectilePrefab;
    private float fireballSpeed = 8f;

    [SerializeField] private GameObject[] flameWallPrefab;

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

    private void FlameWall()
    {
        attackCooltime = 15f;

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
            int attack = Random.Range(2, 3);
            switch (attack)
            {
                case 1:
                    Fireball();
                    break;
                case 2:
                    FlameWall();
                    break;
            }
        }
    }
}
