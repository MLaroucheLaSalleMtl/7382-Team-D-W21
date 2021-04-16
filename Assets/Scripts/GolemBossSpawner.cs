using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject golemBoss;
    [SerializeField] private GameObject bossLock;
    [SerializeField] private GameObject healthBar;
    
    [SerializeField] private PlayerMovement player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Marco
            player.ChangeMusic = true;
            healthBar.SetActive(true);
            healthBar.GetComponent<BossHealthBar>().Boss = golemBoss.GetComponent<GolemBossAI>();
            healthBar.GetComponent<BossHealthBar>().MaxBossHp = 1000;
            healthBar.GetComponent<BossHealthBar>().BossName.text = "GOLEM";

            golemBoss.SetActive(true);
            bossLock.SetActive(true);
            Destroy(gameObject);
        }
    }
}
