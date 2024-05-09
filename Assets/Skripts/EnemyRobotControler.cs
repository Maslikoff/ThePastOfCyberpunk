using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable), typeof(TouchingDirection))]
public class EnemyRobotControler : MonoBehaviour
{
    public float walkSpeed = 50f;
    public float maxSpeed = 3f;
    public float walkStopRate = .05f;
    public DerectionZone attackZone;
    public DerectionZone groundZone;

    private Rigidbody2D _rb;
    private TouchingDirection _touchDirection;
    private Animator _animator;
    private Damageable _damageable;

    public enum WalkaleDirection { Right, Left }

    private WalkaleDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkaleDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkaleDirection.Right)
                    walkDirectionVector = Vector2.right;
                else if (value == WalkaleDirection.Left)
                    walkDirectionVector = Vector2.left;
            }
            _walkDirection = value;
        }
    }

    public bool _hasTerget = false;

    public bool HasTarget
    {
        get { return _hasTerget; }
        private set
        {
            _hasTerget = value;
            _animator.SetBool(AnimatinsStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimatinsStrings.canMove);
        }
    }

    public float AttackColdown
    {
        get
        {
            return _animator.GetFloat(AnimatinsStrings.attackColdown);
        }
        private set
        {
            _animator.SetFloat(AnimatinsStrings.attackColdown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchDirection = GetComponent<TouchingDirection>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = attackZone.colliders.Count > 0;

        if (AttackColdown > 0)
            AttackColdown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_touchDirection.IsGrounded && _touchDirection.IsOnWall)
            FlipDirection();

        if (!_damageable.LockVelocity)
        {
            if (CanMove && _touchDirection.IsGrounded)
                _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x + (walkSpeed * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), _rb.velocity.y);
            else
                _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, walkStopRate), _rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkaleDirection.Right)
            WalkDirection = WalkaleDirection.Left;
        else if (WalkDirection == WalkaleDirection.Left)
            WalkDirection = WalkaleDirection.Right;
        else
            Debug.LogError("Не имеет значения вправо или влево");
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (_touchDirection.IsGrounded)
            FlipDirection();
    }
}
