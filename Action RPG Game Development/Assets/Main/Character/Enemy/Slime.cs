using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Slime : Character
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _healthBar;

    private float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float searchRange = 12f;
    public float attackRange = 2f;

    public float attackCooldown = 2.3f;
    public float attackCooldownTimer = 0;

    public HitBox _hitBox;
    NavMeshAgent agent;

    //[SerializeField] float minPatrolRange = 3f;
    //[SerializeField] float maxPatrolRange = 4f;

    [SerializeField] LocalAudioManager audio;

    protected override void Start()
    {
        base.Start();
        UpdateHealthBar();
        _hitBox._collider.enabled = false;
        _hitBox.damage = attack;
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        if (health > 0)
        {

            base.Update();

            float range = (_player.transform.position - _controller.transform.position).magnitude;

            if (range < searchRange)
            {
                Vector3 targetdirection = _player.transform.position - _controller.transform.position;
                targetdirection.y = 0;
                float angle = Mathf.Atan2(targetdirection.x, targetdirection.z) * Mathf.Rad2Deg;
                float smoothangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(new Vector3(0f, smoothangle, 0f));


                if (attackCooldownTimer <= 0)
                {

                    if (range < attackRange)
                    {
                        attackCooldownTimer = attackCooldown;
                        _animator.CrossFade("Attack", 0.1f);
                        StartCoroutine(Attacking());
                    }
                    else
                    {
                        _animator.SetTrigger("Jump");
                        attackCooldownTimer = 1f;

                        agent.SetDestination(_player.position);
                    }
                }
                else
                {
                    attackCooldownTimer -= Time.deltaTime;
                }

            }
            else
            {

            }

        }

    }

    protected override void FixedUpdate()
    {
        //base.FixedUpdate();
    }

    private IEnumerator Attacking()
    {

        yield return new WaitForSeconds(0.75f);
        _hitBox._collider.enabled = true;

        yield return new WaitForSeconds(0.85f);
        _hitBox._collider.enabled = false;


    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        _canvas.transform.LookAt(_canvas.transform.position + _camera.transform.forward);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    public override void OnDeath()
    {
        base.OnDeath();
        //_animator.SetInteger("DamageType", 2);
        //_animator.SetTrigger("Damage");
        _animator.Play("Damage2");
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();
        _animator.SetInteger("DamageType", 1);
        _animator.SetTrigger("Damage");
        audio.PlaySFX("Slime Bounce");
    }

    public void UpdateHealthBar()
    {
        _healthBar.value = health / maxHealth;
    }

    public void JumpSound()
    {
        audio.PlaySFX("Slime Bounce");
    }

    public void AttackSound()
    {
        audio.PlaySFX("Slime Bounce");
    }

    public void DamageSound()
    {
        
    }

    public void DeathSound()
    {

    }
}
