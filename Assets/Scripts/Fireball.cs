using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 0.75f;

    private Transform fireballPosition;
    [SerializeField] private GameObject explosion;
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, fireballPosition.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag != "Player") Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        fireballPosition = GetComponent<Transform>();
        rigid.velocity = PlayerMovement.playerDirection * speed;
        Destroy(gameObject, lifetime);
    }
}
