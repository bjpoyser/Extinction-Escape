using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMotor motor;
    //[SerializeField] float groundDist;
    //[SerializeField] LayerMask groundMask;

    bool b_gameActive;

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        b_gameActive = GameManager.Instance.b_gameActive;

        PauseInput();
        MoveInput();
        //JumpInput();
    }

    void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool pause = !GameManager.Instance.b_gameActive;
            bool isValid = StateComparitor(GameStateMachine.Instance.gameState);

            if (isValid)
            {
                if (pause)
                {
                    GameManager.Instance.ResumeGame();
                }
                else
                {
                    GameManager.Instance.PauseGame();
                }
            }
        }
    }

    bool StateComparitor(GameState _currState)
    {
        switch (_currState)
        {
            case GameState.play:
                return true;
            case GameState.pause:
                return true;
            case GameState.win:
                return false;
            case GameState.lose:
                return false;
            default:
                return false;
        }
    }

    void MoveInput()
    {
        if (!b_gameActive)
        {
            motor.MovementVelocity(Vector3.zero);
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 moveVel = transform.right * horizontal;

        motor.MovementVelocity(moveVel);
    }

    /*void JumpInput()
    {
        if (!b_gameActive)
        {
            return;
        }

        bool jump = Input.GetButtonDown("Jump");
        bool grounded = Physics.Raycast(transform.position, -transform.up, groundDist, groundMask);

        //Debug.Log(grounded.ToString());

        if (jump && grounded)
        {
            motor.PlayerJump(jump);
        }
    }*/
}
