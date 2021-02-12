using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatEntity
{
    private float hp = 20;
    private float atk = 10;

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (IsDead(hp))
        {
            Die();
        }
    }

    public bool IsDead(float hp)
    {
        return hp <= 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
