using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : Projectile
{
    [SerializeField] private float damage = 15f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.SendMessage("ReceiveDamage", damage);
        }
        else if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "Projectile") Destroy(gameObject);
    }
}
