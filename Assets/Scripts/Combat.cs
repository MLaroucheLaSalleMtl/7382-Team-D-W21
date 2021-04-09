using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private string[] targetsTag;
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < targetsTag.Length; i++)
        {
            if (collider.gameObject.CompareTag(targetsTag[i]))
            {
                collider.SendMessage("ReceiveDamage", damage);
            }
        }
    }
}
