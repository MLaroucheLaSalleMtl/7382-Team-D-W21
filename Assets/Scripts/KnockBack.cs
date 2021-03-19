using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public string[] targetsTag;
    public float KnockBackForce = 10f;
    public float KnockBackTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < targetsTag.Length; i++)
        {
            if (collision.gameObject.CompareTag(targetsTag[i]))
            {
                Rigidbody2D target = collision.GetComponent<Rigidbody2D>();
                if (target != null)
                {
                    target.GetComponent<CombatEntity>().CurrentState = State.stagger;
                    Vector2 difference = target.transform.position - transform.position;
                    difference = difference.normalized * KnockBackForce;
                    target.AddForce(difference, ForceMode2D.Impulse);
                    //StartCoroutine(KnockTime(target));
                }
            }
        }
    }

    private IEnumerator KnockTime(Rigidbody2D target)
    {
        if (target != null)
        {
            yield return new WaitForSeconds(KnockBackTime);
            target.velocity = Vector2.zero;
            target.GetComponent<CombatEntity>().CurrentState = State.idle;
        }
    }
}
