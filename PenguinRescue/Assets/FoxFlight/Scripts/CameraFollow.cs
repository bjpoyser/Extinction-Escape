using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject Player;

    public float cameraSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //X position follow
        Vector3 camPos = transform.position;
        camPos.x = Player.transform.position.x - -9f;
        transform.position = Vector3.Lerp(transform.position, camPos, 3 * Time.fixedDeltaTime);
    }
}
