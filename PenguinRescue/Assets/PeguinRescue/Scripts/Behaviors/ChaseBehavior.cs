using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    #region Constants
    protected const string BOOL_WAITING = "IsWaiting";
    protected const string BOOL_MOVING = "IsMoving";
    #endregion

    [SerializeField] private float _speed;
    [SerializeField] private float _chaseDistance;

    public float current;

    protected Animator _animator;
    protected Rigidbody _rigidBody;
    protected PenguinController _player;

    protected bool _wasSaved;
    protected bool _canMove;

    public bool CanMove { get => _canMove; set => _canMove = value; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        _player = null;
        _animator.SetBool(BOOL_WAITING, false);
        _wasSaved = false;
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PenguinController>();
        if (player != null && !_wasSaved)
        {
            _player = player;
        }
    }

    protected virtual void Move()
    {
        if (_canMove && _player != null)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance > _chaseDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
                Vector3 newRotation = _player.transform.position;
                newRotation.y = transform.position.y;
                transform.LookAt(newRotation);
            }
            else
            {
                PlayerReached();
            }

            _animator.SetBool(BOOL_MOVING, !_player.IsIdle);
        }
        else
        {
            SetIdle();
        }
    }

    protected virtual void PlayerReached(){ }

    protected virtual void SetIdle()
    {
        _animator.SetBool(BOOL_MOVING, false);
    }
}
