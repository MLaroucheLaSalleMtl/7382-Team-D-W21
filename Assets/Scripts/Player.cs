using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CombatEntity
{
    private float _hp = 100;
    private float _atk = 10;

    public float hp { get => _hp; set => _hp = value; }
    public float atk { get => _atk; set => _atk = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 

    }
}
