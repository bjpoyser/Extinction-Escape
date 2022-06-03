using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] beetles;
    public GameObject obstacle;

    private float beetleSpawnTimer = 5f;
    private float obstacleSpawnTimer = 7f;

    // Update is called once per frame
    void Update()
    {
        beetleSpawnTimer -= Time.deltaTime;
        obstacleSpawnTimer -= Time.deltaTime;

        if (beetleSpawnTimer < 0.01)
        {
            SpawnBeetles();
        }
        if (obstacleSpawnTimer < 0.01)
        {
            SpawnOb();
        }
    }

    void SpawnBeetles()
    {
        Instantiate (beetles[(Random.Range(0, beetles.Length))], new Vector3 (player.transform.position.x + 30, Random.Range(2, 8), 0), Quaternion.identity);
        beetleSpawnTimer = Random.Range(1f, 3f);
    }
    void SpawnOb()
    {
        Instantiate(obstacle, new Vector3(player.transform.position.x + 30, 2, 0), Quaternion.identity);
        obstacleSpawnTimer = Random.Range(2, 4);
    }
}
