using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rigid;
    private Animator animator;
    [SerializeField] private Transform playerPosition;

    private Vector2 movement;
    [SerializeField] private float movementSpeed = 10f;

    private bool attack = false;
    private float cooltime = 0f;

    private bool skill = false;
    [SerializeField] private GameObject[] skillList;

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
        if(attack && cooltime <= 0f)
        {
            cooltime = 0.3f;
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    private void Move()
    {
        if (movement != Vector2.zero && (cooltime <= 0f))
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("Moving", true);
            rigid.MovePosition(rigid.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void Skill()
    {
        if(skill && cooltime <= 0f)
        {
            cooltime = 0.3f;
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(skillList[skillIndex], playerPosition.position, rotation);
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
        Move();
        Skill();
        SkillSwitch();
        PlayerDirection();
    }

    private void LateUpdate()
    {
        if (cooltime > 0f) cooltime -= Time.deltaTime;
    }
}
