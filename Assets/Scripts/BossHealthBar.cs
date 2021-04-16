using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private CombatEntity boss;
    [SerializeField] private Image bossHealth;
    private float maxBossHp;
    [SerializeField] private Text bossName;

    public float MaxBossHp { get => maxBossHp; set => maxBossHp = value; }
    public CombatEntity Boss { get => boss; set => boss = value; }
    public Text BossName { get => bossName; set => bossName = value; }
   
    public void UpdateBossHealth()
    {
        bossHealth.fillAmount = Boss.Hp / maxBossHp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateBossHealth();
    }
}
