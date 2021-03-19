using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lifetime;
    [SerializeField] string[] targetsTag;

    public float Damage { get => damage;}
    public float Lifetime { get => lifetime; set => lifetime = value; }
    public string[] TargetsTag { get => targetsTag;}

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);
    }
}
