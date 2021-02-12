using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rigidbody;
    private Animator animator;

    private Vector2 movement;
    [Range(0.1f, 10f)] [SerializeField] private float movementSpeed = 10f;

    private bool attack = false;
    public float cooltime = 0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
            rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        Move();
    }

    private void LateUpdate()
    {
        if (cooltime > 0f) cooltime -= Time.deltaTime;
    }
}
