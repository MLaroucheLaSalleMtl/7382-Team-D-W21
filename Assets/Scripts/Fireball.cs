using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField] private GameObject explosionPrefab;
   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < TargetsTag.Length; i++)
        {
            if (collider.gameObject.CompareTag(TargetsTag[i]))
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collider.gameObject.tag == "Terrain")
            Destroy(gameObject);
    }
}
