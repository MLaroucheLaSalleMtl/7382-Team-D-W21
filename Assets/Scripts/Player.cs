using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CombatEntity
{
    private float maxHp = 100f;
    private float mp = 100f;
    private float maxMp = 100f;
    private float hpRegen = 1f;
    private float mpRegen = 2f;

    public float Mp { get => mp; set => mp = value; }
    public float MaxMp { get => maxMp; set => maxMp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }

    public void HpRegen()
    {
        if(Hp < maxHp)
        {
            Hp += hpRegen * Time.fixedDeltaTime;
        }
    }

    public void MpRegen()
    {
        if (mp < maxMp)
        {
            mp += mpRegen * Time.fixedDeltaTime;
        }
    }
}
