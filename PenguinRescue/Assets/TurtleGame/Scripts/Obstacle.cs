using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Turtle>())
            other.GetComponent<Turtle>().TakeDamage(25);
    }
}
