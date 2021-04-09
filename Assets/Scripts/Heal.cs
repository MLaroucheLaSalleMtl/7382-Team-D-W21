using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private string userTag;
    private Transform user;
    private float lifetime = 0.5f;

    public string UserTag { get => userTag; set => userTag = value; }

    private void Start()
    {
        Destroy(gameObject, lifetime);
        user = GameObject.FindWithTag(UserTag).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = user.transform.position;
    }
}
