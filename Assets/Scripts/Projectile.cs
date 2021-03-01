using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = PlayerMovement.playerDirection * speed;
        Destroy(gameObject, lifetime);
    }
}
