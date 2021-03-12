using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMainFireball : MonoBehaviour
{
    private float lifetime = 0.5f;
    private int splitCount = 6;
    private float splitAngle = 60f;
    private float splitSpeed = 6f;

    private Transform fireballPosition;
    [SerializeField] private GameObject fireballPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "Projectile" && collider.gameObject.tag != "Boss")
        {
            fireballPosition = GetComponent<Transform>();
            Instantiate(fireballPrefab, fireballPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0f)
        {
            fireballPosition = GetComponent<Transform>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
            for(int i = 0; i < splitCount; i++)
            {
                GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
                rigid.AddForce(direction * splitSpeed, ForceMode2D.Impulse);
                direction = Quaternion.AngleAxis(splitAngle, Vector3.forward) * direction;
            }
            Destroy(gameObject);
        }
    }
}
