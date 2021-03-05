using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject bossLock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossAnimator.SetBool("Sleeping", false);
        bossLock.SetActive(true);
        Destroy(gameObject);
    }
}
