using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    public float KnockBackForce = 15f;
    public float KnockBackTime = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = collision.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                player.GetComponent<CombatEntity>().currentState = State.stagger;
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * KnockBackForce;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockTime(player));
            }
        }
    }

    private IEnumerator KnockTime(Rigidbody2D player)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(KnockBackTime);
            player.velocity = Vector2.zero;
            player.GetComponent<CombatEntity>().currentState = State.idle;
        }
    }
}
