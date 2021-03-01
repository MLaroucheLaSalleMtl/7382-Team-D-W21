using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float KnockBackForce = 5f;
    public float KnockBackTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * KnockBackForce;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockTime(enemy));
            }
        }
    }

    private IEnumerator KnockTime(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(KnockBackTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
