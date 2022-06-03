using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Turtle : MonoBehaviour
{
    public Rigidbody rb;
    public Transform turtleBody;
    public Slider hungerbar;
    public Slider healthbar;
    public SegmentSpawner spawner;
    public TextMeshProUGUI scoreText;
    public TurtleGameOver endGame;
    float moveSpeed = 5f;
    float multiplier = 1f;
    float score = 0;
    float health = 100;
    float hunger = 100;
    bool invertedUp;
    Vector2 movement;
    private bool _isPaused;
    bool gameOver = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                invertedUp = !invertedUp;
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical") * (invertedUp ? -1 : 1);
            movement.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 12f;
            }
            else
            {
                moveSpeed = 6f;
            }

            //x move is y rotation
            //y move is -x rotation
            turtleBody.transform.rotation = Quaternion.Slerp(turtleBody.transform.rotation, Quaternion.Euler((-rb.velocity.y * 5) / (Mathf.Clamp(spawner.speed, 0, 12) / 3f), (rb.velocity.x * 5) / (Mathf.Clamp(spawner.speed, 0, 12) / 3f), 0), Time.deltaTime * 8);
            hunger -= Time.deltaTime * 4f;
            hunger = Mathf.Clamp(hunger, 0, 100);
            health = Mathf.Clamp(health, 0, 100);

            healthbar.value = health;
            hungerbar.value = hunger;

            score += Time.deltaTime * moveSpeed * multiplier;
            scoreText.text = "SCORE : " + (int)score;
        }


        if(hunger <= 0 || health <= 0)
        {
            gameOver = true;
            hunger = 100;
            health = 100;
            spawner.EndGame();
            endGame.GameOver((int)score);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(!_isPaused);
            if (_isPaused) PauseMenu.Instance.Show(() => { Pause(!_isPaused); });
            else PauseMenu.Instance.Hide();
        }
    }

    public void Pause(bool pPause)
    {
        if (pPause) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        _isPaused = pPause;
    }
    public void TakeDamage(int damage)
    {
        multiplier = 1f;
        score -= 50;
        health -= damage;
    }
    public void EatFood(int food)
    {
        score += 50 * multiplier;
        multiplier *= 1.1f;
        health += 5;
        hunger += food;
    }
    private void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
