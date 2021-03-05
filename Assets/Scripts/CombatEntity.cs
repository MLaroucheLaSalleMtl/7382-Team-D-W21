using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle, walk, attack, stagger, sleeping
}

public class CombatEntity : MonoBehaviour
{
    [SerializeField] private float hp;
    public State currentState;
    public float staggerTimer = 0.1f;
    public bool invincible = false;

    public float Hp { get => hp; set => hp = value; }

    public virtual void ReceiveDamage(float damage)
    {
        if (!invincible)
        {
            Hp -= damage;
            invincible = true;
            if (IsDead())
            {
                Die();
            }
            StartCoroutine("InvincibilityTimer");
        }
    }

    public IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(0.3f);
        invincible = false;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public void StaggerTimer()
    {
        if(currentState == State.stagger)
        {
            staggerTimer -= 0.1f * Time.fixedDeltaTime;
        }
        if(staggerTimer <= 0f)
        {
            currentState = State.idle;
            staggerTimer = 0.3f;
        }
    }
}
