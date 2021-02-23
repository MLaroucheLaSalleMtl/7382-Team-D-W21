using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack2 : MonoBehaviour
{
    [SerializeField] private float knockback = 1f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Vector3 difference = collider.transform.position - transform.position;
            collider.transform.position += difference.normalized * knockback;
        }
    }
}
