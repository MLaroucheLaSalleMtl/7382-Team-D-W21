using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Player
{
    private Rigidbody2D rigid;
    private Animator animator;

    private Vector2 movement;
    [SerializeField] private float movementSpeed = 500f;

    private bool attack = false;
    private float cooltime = 0f;

    private bool skill = false;
    [SerializeField] private GameObject[] skillList;
    private float skillcooltime = 0f;
    private Vector3 playerDirection;

    [SerializeField] private float fireballMpCost = 20f;
    [SerializeField] private float fireballSpeed = 10f;

    [SerializeField] private float razorLeafMpCost = 10f;
    [SerializeField] private float razorLeafSpeed = 15f;

    [SerializeField] private float earthShieldMpCost = 30f;
    [SerializeField] private int earthShieldCount = 3;

    [SerializeField] private float healMpCost = 50f;
    [SerializeField] private float healAmount = 50f;

    private bool skillSwitch = false;
    private int skillIndex = 0;

    public bool SkillSwitch { get => skillSwitch; set => skillSwitch = value; }
    public int SkillIndex { get => skillIndex; set => skillIndex = value; }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attack = context.performed;
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        skill = context.performed;
    }

    public void OnSkillSwitch(InputAction.CallbackContext context)
    {
        SkillSwitch = context.performed;
    }

    private void Attack()
    {
        if(attack && cooltime <= 0f && CurrentState != State.stagger)
        {
            cooltime = 0.3f;
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    private void Animate()
    {
        if (CurrentState != State.stagger)
        {
            if (movement != Vector2.zero && cooltime <= 0f)
            {
                animator.SetFloat("X", movement.x);
                animator.SetFloat("Y", movement.y);
                animator.SetBool("Moving", true);
                //rigid.MovePosition(rigid.position + movement * movementSpeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("Moving", false);
            }
            Move();
        }
    }

    private void Move()
    {
        if (animator.GetBool("Moving"))
        {
            rigid.velocity = movement.normalized * movementSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void Skill()
    {
        if(skill && cooltime <= 0f && skillcooltime <= 0f && CurrentState != State.stagger)
        {
            switch(SkillIndex)
            {
                case 0:
                    Fireball();
                    break;
                case 1:
                    RazorLeaf();
                    break;
                case 2:
                    EarthShield();
                    break;
                case 3:
                    Heal();
                    break;
            }
        }
    }

    private void Fireball()
    {
        if (Mp >= fireballMpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 1f;
            Mp -= fireballMpCost;
            GameObject fireball = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
            Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
            rigid.AddForce(playerDirection.normalized * fireballSpeed, ForceMode2D.Impulse);
        }
    }

    private void RazorLeaf()
    {
        if (Mp >= razorLeafMpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 1f;
            Mp -= razorLeafMpCost;
            //float angle1 = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            //Quaternion rotation1 = Quaternion.Euler(0f, 0f, angle1);
            GameObject razorLeaf = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
            Rigidbody2D rigid = razorLeaf.GetComponent<Rigidbody2D>();
            rigid.AddForce(playerDirection.normalized * razorLeafSpeed, ForceMode2D.Impulse);
        }
    }

    private void EarthShield()
    {
        if(Mp >= earthShieldMpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 5f;
            Mp -= earthShieldMpCost;
            for (int i = 0; i < earthShieldCount; i++)
            {
                GameObject shield = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
                Orbit script = shield.GetComponent<Orbit>();
                script.Angle = 360f * 0.333f * i;
            }
        }
    }

    private void Heal()
    {
        if(Mp >= healMpCost && Hp <= 100f)
        {
            cooltime = 0.3f;
            skillcooltime = 0.5f;
            Mp -= healMpCost;
            Hp += healAmount;
            GameObject heal = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
            Heal script = heal.GetComponent<Heal>();
            script.UserTag = "Player";
        }
    }

    private void ChangeSkill()
    {
        if (SkillSwitch)
        {
            SkillSwitch = false;
            if (SkillIndex + 1 < skillList.Length)
            {
                SkillIndex++;
            }
            else
            {
                SkillIndex = 0;
            }
        }
    }

    private void PlayerDirection()
    {
        if((movement.x != 0 || movement.y != 0) && cooltime <= 0f)
        {
            playerDirection = new Vector3(movement.x, movement.y, 0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        Animate();
        Skill();
        ChangeSkill();
        PlayerDirection();

        //HpRegen();
        MpRegen();
        StaggerTimer();

        if (cooltime > 0f) cooltime -= Time.deltaTime;
        if (skillcooltime > 0f) skillcooltime -= Time.deltaTime;
    }
}
