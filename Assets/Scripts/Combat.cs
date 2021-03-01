using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag(targetTag))
        {
            collider.SendMessage("ReceiveDamage", damage);
        }
    }
}
