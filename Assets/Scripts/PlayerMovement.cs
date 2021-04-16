using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : Player
{
    private Rigidbody2D rigid;
    private Animator animator;

    private Vector2 movement;
    private float movementSpeed = 500f;

    private bool attack = false;
    private float cooltime = 0f;

    private bool skill = false;
    [SerializeField] private GameObject[] skillList;
    private float skillcooltime = 0f;
    private Vector3 playerDirection;

    private float fireballMpCost = 20f;
    private float fireballSpeed = 10f;
    private int fireballLevel = 1;
    [SerializeField] private GameObject levelTwoFireballPrefab;

    private float razorLeafMpCost = 10f;
    private float razorLeafSpeed = 15f;
    private int razorLeafCount = 3;
    private float razorLeafSpread = 15f;

    private float earthShieldMpCost = 30f;
    private int earthShieldCount = 3;

    private float healMpCost = 50f;
    private float healAmount = 50f;

    private bool skillSwitch = false;
    private int skillIndex = 0;

    //Marco
    [SerializeField] private AudioSource footSteps;
    [SerializeField] private AudioSource swordSwing;
    [SerializeField] private AudioSource healing;
    [SerializeField] private AudioSource fireBallSound;
    [SerializeField] private AudioSource rockShieldSound;
    [SerializeField] private AudioSource razorBladeSound;
    [SerializeField] private AudioSource regularMusic;
    [SerializeField] private AudioSource bossMusic;
    private float walkCount = 0;
    private bool changeMusic = false;
    [SerializeField] private GameObject deathScreen;
    
    public bool SkillSwitch { get => skillSwitch; set => skillSwitch = value; }
    public int SkillIndex { get => skillIndex; set => skillIndex = value; }
    public int FireballLevel { get => fireballLevel; set => fireballLevel = value; }
    public int RazorLeafCount { get => razorLeafCount; set => razorLeafCount = value; }
    public int EarthShieldCount { get => earthShieldCount; set => earthShieldCount = value; }
    public bool ChangeMusic { get => changeMusic; set => changeMusic = value; }

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

            swordSwing.Play();//Marco
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    private void ChangeTheMusic() //Marco
    {
        if (regularMusic.enabled && ChangeMusic)
        {
            regularMusic.volume -= 0.1f * Time.deltaTime;
            if (regularMusic.volume <= 0f)
            {
                regularMusic.enabled = !regularMusic.enabled;
                bossMusic.enabled = !bossMusic.enabled;
                bossMusic.volume = 0.2f;
                bossMusic.Play();
                ChangeMusic = false;
            }
        }

        if (bossMusic.enabled && ChangeMusic)
        {
            bossMusic.volume -= 0.1f * Time.deltaTime;
            if (bossMusic.volume <= 0f)
            {
                bossMusic.enabled = !bossMusic.enabled;
                regularMusic.enabled = !regularMusic.enabled;
                regularMusic.volume = 0.2f;
                regularMusic.Play();
                ChangeMusic = false;
            }
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

                if(walkCount == 0)//Marco
                footSteps.Play();

                //rigid.MovePosition(rigid.position + movement * movementSpeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("Moving", false);

                footSteps.Stop(); //Marco
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
            if (FireballLevel == 1)
            {
                GameObject fireball = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
                Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
                rigid.AddForce(playerDirection.normalized * fireballSpeed, ForceMode2D.Impulse);
            }
            else if (FireballLevel == 2)
            {
                GameObject fireball = Instantiate(levelTwoFireballPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
                rigid.AddForce(playerDirection.normalized * fireballSpeed, ForceMode2D.Impulse);
            }

            fireBallSound.Play();//Marco
        }
    }

    private void RazorLeaf()
    {
        if (Mp >= razorLeafMpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 0.5f;
            Mp -= razorLeafMpCost;
            //float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 skillDirection = Quaternion.AngleAxis(-(razorLeafSpread * ((float)RazorLeafCount - 1f) / 2), Vector3.forward) * playerDirection;
            for (int i = 0; i < RazorLeafCount; i++)
            {
                GameObject razorLeaf = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
                Rigidbody2D rigid = razorLeaf.GetComponent<Rigidbody2D>();
                rigid.AddForce(skillDirection.normalized * razorLeafSpeed, ForceMode2D.Impulse);
                skillDirection = Quaternion.AngleAxis(razorLeafSpread, Vector3.forward) * skillDirection;
                
                razorBladeSound.Play();//Marco
            }
        }
    }

    private void EarthShield()
    {
        if (Mp >= earthShieldMpCost)
        {
            cooltime = 0.3f;
            skillcooltime = 5f;
            Mp -= earthShieldMpCost;
            for (int i = 0; i < EarthShieldCount; i++)
            {
                GameObject shield = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
                Orbit script = shield.GetComponent<Orbit>();
                script.Axis = gameObject.transform;
                script.Angle = (360f / EarthShieldCount) * i;
            }
            
            rockShieldSound.Play();//Marco
        }
    }

    private void Heal()
    {
        if(Mp >= healMpCost && Hp <= 100f)
        {
            cooltime = 0.3f;
            skillcooltime = 0.5f;
            Mp -= healMpCost;
            if (Hp + healAmount > 100f) Hp = 100f;
            else Hp += healAmount;
            GameObject heal = Instantiate(skillList[SkillIndex], transform.position, Quaternion.identity);
            Heal script = heal.GetComponent<Heal>();
            script.UserTag = "Player";
           
            healing.Play();//Marco
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

    public override void Die()
    {
        if (IsDead())
        {
            if (!HasDied)
            {
                HasDied = true;
                animator.SetTrigger("Die");
                rigid.velocity = Vector3.zero;
                rigid.isKinematic = true;

                Invoke("LoadMainMenu", 3f);
            }
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsDead())
        {
            walkCount += Time.deltaTime;//Marco
            if(walkCount >= 0.3)
            {
                walkCount = 0;
            }
           
            Attack();
            Animate();
            Skill();
            ChangeSkill();
            PlayerDirection();

            ChangeTheMusic();

            HpRegen();
            MpRegen();
            StaggerTimer();

            if (cooltime > 0f) cooltime -= Time.deltaTime;
            if (skillcooltime > 0f) skillcooltime -= Time.deltaTime;
        }
        Die();
    }
}
