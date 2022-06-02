using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    public SegmentSpawner spawner;

    private void Update()
    {
        transform.Translate(-Vector3.forward * spawner.speed * Time.deltaTime);
    }
}
