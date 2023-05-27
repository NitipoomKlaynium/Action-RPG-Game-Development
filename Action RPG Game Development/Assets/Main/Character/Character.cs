using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField] protected float gravity = 20.0f;
    [SerializeField] protected float maxFallSpeed = -20.0f;
    [SerializeField] protected float movementSpeed = 3.0f;
    [SerializeField] protected float jumpForce = 2.0f;
    [SerializeField] protected float runningSpeed = 5.5f;

    [SerializeField] protected float groundedOffset = -0.14f;
    [SerializeField] protected float groundRadius = 0.3f;
    [SerializeField] protected LayerMask groundLayers;
    [SerializeField] protected LayerMask obstacleLayers;

    protected bool isGrounded;
    protected Transform _transform;
    protected CharacterController _controller;
    protected Animator _animator;
    protected Vector3 velocity;

    public float maxHealth = 100;
    public float health = 100;
    public float attack = 10;

    protected bool invincible = false;

    protected virtual void Start()
    {
        _transform = GetComponent<Transform>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    //Update is called once per frame
    protected virtual void Update()
    {


    }

    protected virtual void FixedUpdate()
    {
        Gravitation();
        GroundedCheck();
    }

    protected virtual void LateUpdate()
    {

    }

    protected virtual void Gravitation()
    {
        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {

            velocity.y -= gravity * Time.deltaTime;

        }

        _controller.Move(velocity * Time.deltaTime);
    }

    protected void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundRadius, groundLayers, QueryTriggerInteraction.Ignore) || Physics.CheckSphere(spherePosition, groundRadius, obstacleLayers, QueryTriggerInteraction.Ignore);

        //if (isGrounded)
        //{
        //    Collider[] colliders = Physics.OverlapSphere(spherePosition, groundRadius, groundLayers, QueryTriggerInteraction.Ignore);

        //    foreach (Collider collider in colliders)
        //    {
        //        // do something with the collider
        //        Debug.Log("Colliding with " + collider.gameObject.name);
        //    }
        //}

    }

    public virtual void TakeDamage(float damage)
    {
        if (!invincible)
        {
            health -= damage;
        }
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

    }

    public float RandomFloat(float min, float max)
    {
        System.Random random = new System.Random();
        return (float)random.NextDouble() * (max - min) + min;
    }
}
