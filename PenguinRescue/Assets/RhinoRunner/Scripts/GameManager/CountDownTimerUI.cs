using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerUI : MonoBehaviour
{
    Text timerText;

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    private void Update()
    {
        float clampedTime = Mathf.Clamp(GameManager.Instance.levelTime, 0.0f, 99.99f);
        timerText.text = clampedTime.ToString();
    }
}
