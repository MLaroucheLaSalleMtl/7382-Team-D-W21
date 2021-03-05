using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 1.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.tag == "Boss")
        {
            collider.SendMessage("ReceiveDamage", damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
