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
    float moveSpeed = 5f;
    float multiplier = 1f;
    float score = 0;
    float health = 100;
    float hunger = 100;
    Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = -Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 9f;
        }
        else
        {
            moveSpeed = 5f;
        }

        //x move is y rotation
        //y move is -x rotation
        turtleBody.transform.rotation = Quaternion.Slerp(turtleBody.transform.rotation, Quaternion.Euler((-rb.velocity.y * 5) / (spawner.speed /5f), (rb.velocity.x * 5) / (spawner.speed/5f), 0), Time.deltaTime * 8);

        hunger -= Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, 100);
        health = Mathf.Clamp(health, 0, 100);

        healthbar.value = health;
        hungerbar.value = hunger;

        score += Time.deltaTime * moveSpeed * multiplier;
        scoreText.text = "SCORE : " + (int)score;

        if(hunger <= 0 || health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
