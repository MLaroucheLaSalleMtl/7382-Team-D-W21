using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private Transform fireballPosition;
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < TargetsTag.Length; i++)
        {
            if (collider.gameObject.CompareTag(TargetsTag[i]))
            {
                fireballPosition = GetComponent<Transform>();
                Instantiate(explosionPrefab, fireballPosition.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collider.gameObject.tag == "Terrain")
            Destroy(gameObject);
    }
}
