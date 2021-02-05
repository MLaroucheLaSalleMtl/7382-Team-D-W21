using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int atk;

    public CombatEntity(int maxHp, int hp, int atk)
    {
        this.hp = hp;
        this.maxHp = hp;
        this.atk = atk;
    }

    public void ReceiveDamage(int damage)
    {
        this.hp -= damage;
        if(IsDead())
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return this.hp <= 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
