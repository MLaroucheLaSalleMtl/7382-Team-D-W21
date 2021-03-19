using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    [SerializeField] private string[] targetTags;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 1.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null)
        {
            for(int i = 0; i < targetTags.Length; i++)
            {
                if(collider.gameObject.CompareTag(targetTags[i])) collider.SendMessage("ReceiveDamage", damage);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
