using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle, walk, attack, stagger
}

public class CombatEntity : MonoBehaviour
{
    [SerializeField] private float hp;
    private State currentState;
    private float starggerDuration = 0.3f;
    private float staggerTimer = 0.3f;
    private bool invincible = false;

    public float Hp { get => hp; set => hp = value; }
    public State CurrentState { get => currentState; set => currentState = value; }

    public virtual void ReceiveDamage(float damage)
    {
        if (!invincible)
        {
            hp -= damage;
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
        if(CurrentState == State.stagger)
        {
            staggerTimer -= 0.3f * Time.fixedDeltaTime;
        }
        if(staggerTimer <= 0f)
        {
            CurrentState = State.idle;
            staggerTimer = starggerDuration;
        }
    }
}
