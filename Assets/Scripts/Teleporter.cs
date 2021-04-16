using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Vector2 targetLocation;
    [SerializeField] private AudioSource teleportSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = targetLocation;
            if (teleportSound != null)
            {
                teleportSound.Play();
            }
        }
    }
}
