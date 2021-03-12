using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : MonoBehaviour
{
    private float lifetime = 1f;
    private Transform fireballPosition;
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "Projectile" && collider.gameObject.tag != "Boss")
        {
            fireballPosition = GetComponent<Transform>();
            Instantiate(explosionPrefab, fireballPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if(lifetime <= 0f)
        {
            fireballPosition = GetComponent<Transform>();
            Instantiate(explosionPrefab, fireballPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
