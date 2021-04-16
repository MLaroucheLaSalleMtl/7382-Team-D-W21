using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image manaBar;

    public void UpdateHealth()
    {
        healthbar.fillAmount = player.Hp / player.MaxHp;
        manaBar.fillAmount = player.Mp / player.MaxMp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateHealth();
    }
}
