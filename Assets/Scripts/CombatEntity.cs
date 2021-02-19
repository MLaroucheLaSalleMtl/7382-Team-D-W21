using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    //public virtual void ReceiveDamage(float damage, float hp)
    //{
    //    hp -= damage;
    //    if (IsDead(hp))
    //    {
    //        Die();
    //    }
    //}

    public bool IsDead(float hp)
    {
        return hp <= 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
