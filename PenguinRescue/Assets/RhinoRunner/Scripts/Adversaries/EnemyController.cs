using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float timeToGive = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.AdjustTime(timeToGive);
            gameObject.SetActive(false);
        }
    }
}
