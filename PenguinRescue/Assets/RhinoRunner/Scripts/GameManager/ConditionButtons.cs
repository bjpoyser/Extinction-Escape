using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionButtons : MonoBehaviour
{
    public void ContinueButton()
    {
        SceneManager.LoadScene("RhinoRunner_Metrics");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("RhinoRunner_Metrics");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
