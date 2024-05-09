using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DerectionZone biteZone;
    public List<Transform> waypoints;

    private Animator _animator;
    private Rigidbody2D _rb;
    private Damageable _damageable;

    private Transform _nextWaypoint;
    private int _waipointNum = 0;

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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _nextWaypoint = waypoints[_waipointNum];
    }

    private void Update()
    {
        HasTarget = biteZone.colliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (_damageable.IsAlive)
        {
            if (CanMove)
                Flight();
            else
                _rb.velocity = Vector3.zero;
        }
        else
        {
            _rb.gravityScale = 2f;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (_nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(_nextWaypoint.position, transform.position);

        _rb.velocity = directionToWaypoint * flightSpeed;

        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            _waipointNum++;

            if (_waipointNum >= waypoints.Count)
                _waipointNum = 0;

            _nextWaypoint = waypoints[_waipointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            if (_rb.velocity.x < 0)
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
        }
        else
        {
            if (_rb.velocity.x > 0)
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
        }
    }
}
