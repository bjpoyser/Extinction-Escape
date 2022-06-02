using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca : MonoBehaviour
{
    [SerializeField] private float _eatSpeed;
    private Vector3 _initialPositon;
    private Vector3 _TargetPosition;
    private bool _reached;

    private void Update()
    {
        if (_reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, _initialPositon, _eatSpeed * Time.deltaTime);
        }
        else
        {
            float distance = Vector3.Distance(_TargetPosition, transform.position);
            if (distance > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, _TargetPosition, _eatSpeed * Time.deltaTime);
            }
            else
            {
                _reached = true;
            }
        }   
    }

    public void SetTarget(Vector3 pTargetPosition, Vector3 pInitialPosition)
    {
        _TargetPosition = pTargetPosition;
        _initialPositon = pInitialPosition;
        _reached = false;
    }
}
