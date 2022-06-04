using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Start()
    {
        if( _target == null)
        {
            _target = Camera.main.transform;
        }
    }

    private void Update()
    {
        transform.LookAt(_target);
    }
}
