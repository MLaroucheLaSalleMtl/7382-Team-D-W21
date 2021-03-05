using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CombatEntity
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float mp = 100f;
    [SerializeField] private float maxMp = 100f;

    public float Mp { get => mp; set => mp = value; }
    public float MaxMp { get => maxMp; set => maxMp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }

    public override void Die()
    {
        
    }

    public void HpRegen()
    {
        if(Hp < maxHp)
        {
            Hp += 0.5f * Time.fixedDeltaTime;
        }
    }

    public void MpRegen()
    {
        if (mp < maxMp)
        {
            mp += 0.5f * Time.fixedDeltaTime;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
