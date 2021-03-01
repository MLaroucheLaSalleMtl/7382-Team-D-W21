using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public float hp;

    public virtual void ReceiveDamage(float damage)
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
}
