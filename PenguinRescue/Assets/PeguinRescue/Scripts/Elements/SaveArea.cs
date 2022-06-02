using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class SaveArea : MonoBehaviour
{
    [SerializeField] private TargetTags _targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag.ToString()))
        {
            BabyPenguin babyPenguin = other.GetComponent<BabyPenguin>();
            if (babyPenguin != null)
            {
                babyPenguin.Save();
            }
        }
    }
}
