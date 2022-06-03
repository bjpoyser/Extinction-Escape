using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seal : MonoBehaviour
{
    [SerializeField] LayerMask _ground, _prey;
    
    [Header("Ranges")]
    [SerializeField] private float attackRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float _walkPointRange;

    [Header("Speed")]
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _patrolSpeed;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;

    private Vector3 _walkPoint;
    private bool _walkPoinSet;

    private bool _inAttackRange;
    private bool _inChaseRange;
    private bool _canMove;

    private void Awake()
    {
        _canMove = false;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        StartCoroutine(WaitToStartAgain(5));
    }

    private void Update()
    {
        if (_canMove)
        {
            _inAttackRange = Physics.CheckSphere(transform.position, attackRange, _prey);
            _inChaseRange = Physics.CheckSphere(transform.position, chaseRange, _prey);

            if (_target == null || !_inChaseRange)
            {
                _target = null;
                var hits = Physics.SphereCastAll(transform.position, chaseRange, transform.forward, chaseRange, _prey);
                for (int i = 0; i < hits.Length; i++)
                {
                    BabyPenguin babyPenguin = hits[i].transform.GetComponent<BabyPenguin>();
                    if (babyPenguin != null && !babyPenguin.WasCaptured)
                    {
                        _target = babyPenguin.transform;
                    }
                    else
                    {
                        PenguinController playerPenguin = hits[i].transform.GetComponent<PenguinController>();
                        if (playerPenguin != null && !playerPenguin.WasCaptured)
                        {
                            _target = playerPenguin.transform;
                        }
                    }
                }
            }

            if (!_inChaseRange && !_inAttackRange) Patrol();
            if (_inChaseRange && !_inAttackRange) Chase();
            if (_inChaseRange && _inAttackRange) Attack();
        }
    }

    private void Patrol()
    {
        _agent.speed = _patrolSpeed;

        if (!_walkPoinSet) GetWalkPoint();
        if (_walkPoinSet) _agent.SetDestination(_walkPoint);
        Vector3 distance =  transform.position - _walkPoint;
        if (distance.magnitude < 1f)
        {
            _walkPoinSet = false;
        }
    }

    private void GetWalkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _ground))
        {
            _walkPoinSet = true;
        }
    }

    private void Chase()
    {
        if (_target != null)
        {
            _agent.speed = _chaseSpeed;
            _agent.SetDestination(_target.position);
        }
    }

    public void Attack()
    {
        _agent.SetDestination(transform.position);
        if (_target != null)
        {
            _canMove = false;
            transform.LookAt(_target);
            _animator.SetTrigger("Attack");
            PenguinController playerPenguin = _target.GetComponent<PenguinController>();
            if (playerPenguin != null && !playerPenguin.WasCaptured)
            {
                playerPenguin.CanMove = false;
                playerPenguin.WasCaptured = true;
                tempBabyPenguin = null;
                tempPlayer = playerPenguin;
            }
            else
            {
                BabyPenguin babyPenguin = _target.GetComponent<BabyPenguin>();
                if (babyPenguin != null && !babyPenguin.WasCaptured)
                {
                    babyPenguin.CanMove = false;
                    babyPenguin.WasCaptured = true;
                    tempBabyPenguin = babyPenguin;
                    tempPlayer = null;
                }
            }
        }
    }

    PenguinController tempPlayer;
    BabyPenguin tempBabyPenguin;

    public void KillTarget()
    {
        if (tempPlayer != null)
        {
            tempPlayer.Trapped();
            tempPlayer = null;
        }else if (tempBabyPenguin)
        {
            tempBabyPenguin.Trapped();
            tempBabyPenguin = null;
        }
        else
        {
            Destroy(_target.gameObject);
        }
        
        _target = null;
    }

    public void EnableMovement()
    {
        StartCoroutine(WaitToStartAgain(2));
    }

    IEnumerator WaitToStartAgain(float pTime)
    {
        _animator.SetBool("IsIdle", true);
        yield return new WaitForSeconds(pTime);
        _canMove = true;
        yield return null;
        _animator.SetBool("IsIdle", false);
    }
}
