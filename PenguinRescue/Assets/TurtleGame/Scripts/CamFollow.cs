using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform targetPoint;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint.position, Time.deltaTime * 8f);
    }
}
