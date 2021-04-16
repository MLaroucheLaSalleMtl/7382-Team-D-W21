using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogBossSpawner : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject bossLock;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject logBoss;

    [SerializeField] private PlayerMovement player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Marco
            player.ChangeMusic = true;
            healthBar.SetActive(true);
            healthBar.GetComponent<BossHealthBar>().Boss = logBoss.GetComponent<LogBossAI>();
            healthBar.GetComponent<BossHealthBar>().MaxBossHp = 500;
            healthBar.GetComponent<BossHealthBar>().BossName.text = "THE LOG";            

            bossAnimator.SetBool("Sleeping", false);
            bossLock.SetActive(true);
            Destroy(gameObject);
        }
    }
}
