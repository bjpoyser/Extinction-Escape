using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    Vector3 velocity;
    public float speed;
    [SerializeField] float speedMultiple;
    CharacterController charCon;

    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
        velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.b_gameActive)
        {
            PerformMovement();
        }
    }

    public void MovementVelocity(Vector3 moveDelta)
    {
        //moveDelta += transform.forward * (speed * speedMultiple);
        velocity = moveDelta * (speed * speedMultiple) * Time.fixedDeltaTime;
    }

    void PerformMovement()
    {
        charCon.Move(velocity);
    }

    /*public void PlayerJump(bool jumpDelta)
    {
        Debug.Log("Do the Jump thing");
    }*/
}
