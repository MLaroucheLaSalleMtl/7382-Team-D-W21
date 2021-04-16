using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMainFireball : Projectile
{
    private int splitCount = 6;
    private float splitAngle = 60f;
    private float splitSpeed = 10f;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject fireballPrefab;

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

    private void FireballSplit()
    {
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

    private void FixedUpdate()
    {
        Lifetime -= Time.fixedDeltaTime;
        if (Lifetime <= 0f)
        {
            FireballSplit();
        }
    }
}
