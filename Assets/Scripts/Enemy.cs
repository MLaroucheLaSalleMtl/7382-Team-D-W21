using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle, walk, attack, stagger
}

public class Enemy : CombatEntity
{
    public EnemyState currentState;
    public float hp = 30;
    public float atk = 10;

    private Rigidbody2D rigid;
    public float kbduration = 0f;

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (IsDead(hp))
        {
            Die();
        }
    }

    //public void Knockback(Vector3 vector)
    //{
    //    kbduration = 1f;
    //    rigid.isKinematic = false;
    //    Vector2 difference = new Vector2(transform.position.x + vector.x, transform.position.y + vector.y);
    //    rigid.AddForce(difference.normalized, ForceMode2D.Impulse);
    //}

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(kbduration >= 0f)
        //{
        //    kbduration -= Time.deltaTime;
        //}
        //else
        //{
        //    rigid.isKinematic = true;
        //}
    }
}
