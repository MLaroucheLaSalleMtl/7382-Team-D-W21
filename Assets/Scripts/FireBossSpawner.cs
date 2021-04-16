using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireBoss;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject bossLock;
    [SerializeField] private GameObject healthBar;
    private bool activated = false;

    [SerializeField] private PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SpawnFireBoss());
        }
    }

    private IEnumerator SpawnFireBoss()
    {
        if (!activated)
        {
            activated = true;
            bossLock.SetActive(true);

            //Marco
            player.ChangeMusic = true;
            healthBar.SetActive(true); 
            healthBar.GetComponent<BossHealthBar>().Boss = fireBoss.GetComponent<FireBossAI>();
            healthBar.GetComponent<BossHealthBar>().MaxBossHp = 500;
            healthBar.GetComponent<BossHealthBar>().BossName.text = "FIRE MAGE";

            Instantiate(explosion, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
            fireBoss.SetActive(true);
            Destroy(gameObject);
        }
    }
}