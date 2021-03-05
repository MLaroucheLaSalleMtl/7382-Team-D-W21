using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Player player;
    public Image healthbar;
    public Image manaBar;

    // Start is called before the first frame update
    void Start()
    {
       
    }

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
