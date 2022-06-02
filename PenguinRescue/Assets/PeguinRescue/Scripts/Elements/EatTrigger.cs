using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class EatTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TargetTags.Penguin.ToString()))
        {
            other.GetComponent<PenguinController>().Trapped();
        }
        else if (other.CompareTag(TargetTags.BabyPenguin.ToString()))
        {
            other.GetComponent<BabyPenguin>().Trapped();
        }
    }
}
