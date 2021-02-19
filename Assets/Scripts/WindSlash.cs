using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.SendMessage("ReceiveDamage", damage);
        }
        else if (collider.gameObject.tag != "Player") Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = PlayerMovement.playerDirection * speed;
        Destroy(gameObject, lifetime);
    }
}
