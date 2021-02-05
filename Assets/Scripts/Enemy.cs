using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatEntity
{
    public Enemy(int maxHp, int hp, int atk) : base(maxHp, hp, atk)
    {
        this.hp = 20;
        this.atk = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
