using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class PlayerControler : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _isRunning;
    [SerializeField] private bool _isFacingRight;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _moveInput;
    private TouchingDirection _touchDir;
    private Damageable _damageable;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving)   //&& !_touchDir.IsOnWall
                {
                    if (_touchDir.IsGrounded)
                    {
                        if (IsRunning)
                            return runSpeed;
                        else
                            return walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;   // скорость в покое = 0
                }
            }
            else return 0;
        }
    }

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimatinsStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            _animator.SetBool(AnimatinsStrings.isRunning, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
                transform.localScale *= new Vector2(-1, 1);

            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimatinsStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get { return _animator.GetBool(AnimatinsStrings.isAlive); }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchDir = GetComponent<TouchingDirection>();
        _damageable = GetComponent<Damageable>();
    }

    void FixedUpdate()
    {
        if (!_damageable.LockVelocity)
            _rb.velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rb.velocity.y);

        _animator.SetFloat(AnimatinsStrings.yVelocity, _rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = _moveInput != Vector2.zero;

            SetFacingDirection(_moveInput);
        }
        else
            IsMoving = false;
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x < 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x > 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;
        else if (context.canceled)
            IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _touchDir.IsGrounded && CanMove)
        {
            _animator.SetTrigger(AnimatinsStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            _animator.SetTrigger(AnimatinsStrings.attackTrigger);
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }

    public void OnAttackCun(InputAction.CallbackContext context)
    {
        if (context.started)
            _animator.SetTrigger(AnimatinsStrings.attackGunTrigger);
    }
}
