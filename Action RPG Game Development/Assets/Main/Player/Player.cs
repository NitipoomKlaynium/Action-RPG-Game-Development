using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private TMP_Text text;
    [SerializeField] private LocalAudioManager sfx;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;

    private float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    private bool isJumping = false;
    private bool isDashing = false;
    private bool isFighting = false;

    private bool isAction = false;
    private float attackCooldown = 0f;
    private float fightingTimer = 0f;
    private bool canMove = true;

    private float inputBufferTime = 0.2f;
    private float leftMouseBuffer = 0f;

    public HurtBox _hurtBox;
    public HitBox _hitBox;

    private float maxStamina = 100f;
    private float stamina = 100f;
    private float staminaRegeneration = 14f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CinemachineCore.GetInputAxis = GetInputAxis;
        _hitBox._collider.enabled = false;


        maxHealth += PlayerPrefs.GetInt("Health") * 20f;
        health = maxHealth;
        maxStamina += PlayerPrefs.GetInt("Stamina") * 20f;
        stamina = maxStamina;
        staminaRegeneration = (14f / 100f) * maxStamina;
        attack += PlayerPrefs.GetInt("Attack") * 5;

        _hitBox.damage = attack;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        PlayerAction();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            toggleMouseLock();
        }

        stamina += staminaRegeneration * Time.deltaTime;
        healthBar.fillAmount = health / maxHealth;
        staminaBar.fillAmount = stamina / maxStamina;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _animator.CrossFade("Damage", 0.1f);
        _animator.SetBool("isFighting", true);
        isFighting = true;
        fightingTimer = 10f;
    }
    private void PlayerAction()
    {
        if (isFighting)
        {
            if (fightingTimer > 0)
            {
                fightingTimer -= Time.deltaTime;
            }
            else
            {
                fightingTimer = 0;
                isFighting = false;
                _animator.SetBool("isFighting", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            isJumping = true;
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            _animator.CrossFade("JumpStart", 0.1f);
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        //Debug.Log(_hurtBox.isEnable);
        if (leftMouseBuffer > 0)
        {
            leftMouseBuffer -= Time.deltaTime;
            if (isGrounded && attackCooldown <= 0 && stamina > 38)
            {
                stamina -= 38;
                fightingTimer = 10f;
                attackCooldown = 1f;
                _animator.SetBool("isAttacking", true);
                _animator.CrossFade("Attack 1", 0.1f);
                sfx.PlaySFX("Sword Swing");
                StartCoroutine(Attacking());
            }
        }

        if (Input.GetMouseButton(0))
        {
            leftMouseBuffer = inputBufferTime;
        }

        Move();
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.2f);
        _hitBox._collider.enabled = true;

        yield return new WaitForSeconds(0.3f);
        _hitBox._collider.enabled = false;

        _animator.SetBool("isAttacking", false);
    }

    protected override void Gravitation()
    {
        base.Gravitation();
        if (isJumping && isGrounded)
        {
            isJumping = false;
            _animator.CrossFade("Jump Land", 0.1f);
            StartCoroutine(Landing());
        }
    }

    private IEnumerator Landing()
    {
        canMove = false;
        yield return new WaitForSeconds(0.2f);
        if (isFighting)
        {
            _animator.CrossFade("Arm Idle", 0.1f);
        }
        else
        {

            _animator.CrossFade("Idle", 0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    private void TurnPlayerToCamera()
    {
        float targetAngle = mainCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(_controller.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        _controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (horizontalInput > 0)
        {
            _animator.SetInteger("Direction", 0); // Right
        }
        else if (horizontalInput < 0)
        {
            _animator.SetInteger("Direction", 1); // Front
        }
        else if (verticalInput > 0)
        {
            _animator.SetInteger("Direction", 2); // Left
        }
        else if (verticalInput < 0)
        {
            _animator.SetInteger("Direction", 3); // Back
        }

        if (inputDirection.magnitude > 0.1)
        {
            
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_controller.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (!isAction)
            {
                _controller.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (isDashing == false && attackCooldown <= 0)
                {
                    if (canMove)
                    {
                        if (Input.GetKey(KeyCode.C) && stamina > 60)
                        {
                            stamina -= 60;
                            isDashing = true;
                            isFighting = true;
                            fightingTimer = 10;
                            _animator.SetBool("isFighting", true);
                            StartCoroutine(Dash(moveDirection));
                        }
                        else if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
                        {
                            stamina -= 18 * Time.deltaTime;
                            _controller.Move(moveDirection * runningSpeed * Time.deltaTime);
                            _animator.SetFloat("Speed", runningSpeed);
                        }
                        else
                        {
                            _controller.Move(moveDirection * movementSpeed * Time.deltaTime);
                            _animator.SetFloat("Speed", movementSpeed);
                        }
                    }
                }
            }
            
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        
    }

    private IEnumerator Dash(Vector3 direction)
    {
        invincible = true;
        _animator.CrossFade("Dash", 0.1f);
        yield return new WaitForSeconds(0.1f);

        velocity = direction * 12;

        yield return new WaitForSeconds(0.3f);

        velocity = Vector3.zero;

        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }

    private void toggleMouseLock()
    {
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public float GetInputAxis(string axis)
    {
        if (Cursor.visible)
        {
            return 0.0f;
        }
        return UnityEngine.Input.GetAxis(axis);
    }
}
