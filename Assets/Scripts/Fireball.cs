using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private Transform fireballPosition;
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.tag == "Boss")
        {
            fireballPosition = GetComponent<Transform>();
            Instantiate(explosionPrefab, fireballPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "Projectile") Destroy(gameObject);
    }
}
