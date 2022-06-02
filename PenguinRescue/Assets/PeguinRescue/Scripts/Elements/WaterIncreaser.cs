using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIncreaser : MonoBehaviour
{
    [SerializeField] private float _freq;
    [SerializeField] private float _amount;
    [SerializeField] private float _increasingSpeed;

    [SerializeField] private float _maxHeight;

    private float _increaseTimer;
    private bool _increaseEnabled;

    Vector3 newPos;

    private void Start()
    {
        newPos = transform.position;
        _increaseEnabled = true;
        _increaseTimer = Time.time + _freq;
    }

    void Update()
    {
        if (_increaseEnabled)
        {
            if (_increaseTimer < Time.time)
            {
                _increaseTimer = Time.time + _freq;
                float newY = transform.position.y + _amount;
                newPos = new Vector3(transform.position.x, newY, transform.position.z);
            }

            if (transform.position.y < _maxHeight)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPos, _increasingSpeed * Time.deltaTime);
            }
        }
        else
        {
            _increaseTimer = Time.time + _freq;
        }
    }
}
