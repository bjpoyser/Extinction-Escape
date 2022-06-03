using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Beetle")
        {
            Destroy(trig.gameObject);
        }
    }
}
