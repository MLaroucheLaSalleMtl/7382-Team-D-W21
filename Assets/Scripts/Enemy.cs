using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle, walk, attack, stagger
}

public class Enemy : CombatEntity
{
    public EnemyState currentState;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
