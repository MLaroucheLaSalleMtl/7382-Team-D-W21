using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    [SerializeField] private Transform impactPoint;
    private Animator animator;
    private Rigidbody2D rigid;
    private CircleCollider2D col;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float knockbackForce = 10f;
    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, impactPoint.position) <= 0.5f)
        {
            animator.SetTrigger("Broken");
            rigid.gravityScale = 0f;
            rigid.velocity = Vector3.zero;
            if (!activated)
            {
                activated = true;
                Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, col.radius * 2.5f, layer);
                for (int i = 0; i < target.Length; i++)
                {
                    target[i].SendMessage("ReceiveDamage", damage);
                    target[i].GetComponent<CombatEntity>().CurrentState = State.stagger;
                    Rigidbody2D targetrigid = target[i].GetComponent<Rigidbody2D>();
                    Vector2 direction = target[i].transform.position - transform.position;
                    targetrigid.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
                }
            }
            Destroy(rock, 0.4f);
        }
    }
}
