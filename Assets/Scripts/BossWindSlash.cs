using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWindSlash : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage = 5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.SendMessage("ReceiveDamage", damage);
        }
        else if (collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "Projectile") Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
