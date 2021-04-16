using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : Projectile
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        for(int i = 0; i < TargetsTag.Length; i++)
        {
            if (collider.gameObject.CompareTag(TargetsTag[i]))
            {
                collider.SendMessage("ReceiveDamage", Damage);
            }
        }

        if (collider.gameObject.tag == "Terrain" || collider.gameObject.tag == "Fire")
            Destroy(gameObject);
    }
}
