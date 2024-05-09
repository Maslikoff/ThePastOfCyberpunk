using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private float groundDistense = .05f;
    [SerializeField] private float wallDistance = .2f;
    [SerializeField] private float ceilingDistance = .05f;

    private RaycastHit2D[] groudHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private Rigidbody2D _rb;
    private CapsuleCollider2D _touchingCol;
    private Animator _animator;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;

    /// <summary>
    /// Проверка касания земли
    /// </summary>
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimatinsStrings.isGrounded, value);
        }
    }

    private Vector2 wallCheckDir => gameObject.transform.localPosition.x > 0 ? Vector2.right : Vector2.left;

    /// <summary>
    /// Проверка касания стены
    /// </summary>
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimatinsStrings.isOnWall, value);
        }
    }

    /// <summary>
    /// Проверка касания потолка
    /// </summary>
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimatinsStrings.isOnCeiling, value);
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingCol = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = _touchingCol.Cast(Vector2.down, contactFilter, groudHits, groundDistense) > 0;
        IsOnWall = _touchingCol.Cast(wallCheckDir, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = _touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
