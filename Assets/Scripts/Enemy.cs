using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatEntity
{
    public float hp = 30;
    public float atk = 10;

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
        if (IsDead(hp))
        {
            Die();
        }
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
