using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCol : MonoBehaviour
{
    public GameOver GameOverScreen;
    public int score = 0;
    public Text pointsText;

    void Update()
    {
        pointsText.text = score + " Beetles";
    }    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Dog"){
            GameOver();
        }
    }
    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Beetle")
        {
            //increase score
            score = score + 1;
            Destroy(trig.gameObject);
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverScreen.Setup(score);
    }
}
