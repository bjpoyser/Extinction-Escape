using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PenguinController : MonoBehaviour
{
    #region Constants
    private const string BOOL_WAITING = "IsWaiting";
    private const string BOOL_MOVING = "IsMoving";
    #endregion

    #region Inspector Variables
    [Header("Stats")]
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _walkAcceleration;
    [SerializeField] private float _sliddingAcceleration;
    [SerializeField] private float _swimmingAcceleration;

    [Space(10)]
    [SerializeField] private float _currentVel;
    #endregion

    #region Private Variables
    private static PenguinController _instance;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private bool _canMove;
    private bool _wasCaptured;
   /* private bool _isSlidding;
    private bool _isSwimming;*/

    private bool _isIdle;

    private Vector3 _direction;

    private float _currentSpeed;
    private float _rotationSmoothTime;

    private float _nextWaiting;

    public bool IsIdle { get => _isIdle; set => _isIdle = value; }
    public bool CanMove { get => _canMove; set => _canMove = value; }
    public bool WasCaptured { get => _wasCaptured; set => _wasCaptured = value; }
    public static PenguinController Instance { get => _instance; set => _instance = value; }
    #endregion

    #region Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        _instance = this;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _canMove = true;
        /*_isSlidding = false;
        _isSwimming = false;*/
    }

    private void Update()
    {
        if (_canMove)
        {
            Move();
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if (_isIdle)
        {
            if(!_animator.GetBool(BOOL_WAITING) & _nextWaiting < Time.time)
            {
                _animator.SetBool(BOOL_WAITING, true);
            }
        }
        else _nextWaiting = Time.time + 1;

        _animator.SetBool(BOOL_MOVING, !_isIdle);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * _currentSpeed, ForceMode.Acceleration);
        if (_rigidbody.velocity.magnitude > _currentSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
        }
    }

    private void Move()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        _currentSpeed = _walkAcceleration;
        _direction = new Vector3(hAxis, 0, vAxis).normalized;

        if (_direction.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSmoothTime, _rotationForce);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if(vAxis == 0 && hAxis == 0)
        {
            _isIdle = true;
        }
        else
        {
            _isIdle = false;
            TurnOffWaiting();
        }
    }

    public void TurnOffWaiting()
    {
        _animator.SetBool(BOOL_WAITING, false);

        float RandomNextWait = Random.Range(2, 6);
        _nextWaiting = Time.time + RandomNextWait;
    }

    public void Trapped()
    {
        PenguinGameManager.Instance.GameOver(2);
        Destroy(gameObject);
    }
    #endregion
}
