using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CombatEntity
{
    public Player(int maxHp, int hp, int atk) : base(maxHp, hp, atk)
    {
        this.hp = 100;
        this.atk = 10;
    }

    private bool basicAttack;
    [SerializeField] private Collider2D basicHitbox;

    public void onBasicAttack(InputAction.CallbackContext context)
    {
        basicAttack = context.performed;
    }

    private void BasicAttack()
    {
        if(basicAttack)
        {
            //LaunchAttack(basicHitbox, this.atk);
            Debug.Log("Test");
        }
    }

    //private void LaunchAttack(Collider2D col, int damage)
    //{
    //    Collider2D[] cols = Physics2D.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("PlayerHitbox"));
    //    foreach (Collider2D c in cols)
    //    {
    //        c.SendMessageUpwards("ReceiveDamage", damage);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BasicAttack();
    }
}
