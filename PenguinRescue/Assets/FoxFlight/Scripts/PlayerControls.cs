using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2f;

    public bool isGrounded;
    Rigidbody rb;

    private bool _isPaused;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 2f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
}
