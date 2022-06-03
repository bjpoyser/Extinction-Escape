using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Turtle>())
        {
            other.GetComponent<Turtle>().EatFood(10);
            Destroy(gameObject);
        }

    }
}
