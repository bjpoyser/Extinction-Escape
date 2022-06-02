using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject foodPrefab;
    SegmentSpawner segmentSpawner;

    private void Start()
    {
        segmentSpawner = FindObjectOfType<SegmentSpawner>();
        SpawnObstacle();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Turtle>())
        {
            segmentSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }
    private void Update()
    {
        transform.Translate(-Vector3.forward * segmentSpawner.speed * Time.deltaTime);
    }
    void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(1,10);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        int foodChance = Random.Range(0, 2);
        if(foodChance == 1)
        {
            Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }
}
