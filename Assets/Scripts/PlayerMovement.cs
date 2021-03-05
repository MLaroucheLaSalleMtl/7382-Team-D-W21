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
    private float mpCost = 20f;

    private bool skillSwitch = false;
    public int skillIndex = 0;

    public static Vector3 playerDirection;

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
        skillSwitch = context.performed;
    }

    private void Attack()
    {
        if(attack && cooltime <= 0f && currentState != State.stagger)
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
        if (currentState != State.stagger)
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
        if(skill && cooltime <= 0f && skillcooltime <= 0f && currentState != State.stagger && Mp >= mpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 1f;
            Mp -= 20f;
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(skillList[skillIndex], transform.position, rotation);
        }
    }

    private void SkillSwitch()
    {
        if (skillSwitch)
        {
            skillSwitch = false;
            if (skillIndex + 1 < skillList.Length)
            {
                skillIndex++;
            }
            else
            {
                skillIndex = 0;
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
        SkillSwitch();
        PlayerDirection();

        HpRegen();
        MpRegen();
        StaggerTimer();

        if (cooltime > 0f) cooltime -= Time.deltaTime;
        if (skillcooltime > 0f) skillcooltime -= Time.deltaTime;
    }
}
