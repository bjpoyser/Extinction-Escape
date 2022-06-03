using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject segment;
    public float speed = 3f;
    public int numSegments = 0;
    GameObject lastObject;
    bool endGame;

    public void SpawnTile()
    {
        numSegments++;
        if (lastObject != null)
        {
            GameObject temp = Instantiate(segment, lastObject.transform.GetChild(0).transform.position, Quaternion.identity);
            lastObject = temp;
        }
        else
        {
            GameObject temp = Instantiate(segment, Vector3.zero, Quaternion.identity);
            lastObject = temp;
        }
        lastObject.GetComponent<Segment>().segmentIndex = numSegments;
        lastObject.GetComponent<Segment>().SpawnObstacle();
    }
    private void Update()
    {
        if(endGame == false)
        {
            speed += Time.deltaTime * 0.25f;
        }
        else
        {
            speed = 0;
        }
    }
    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }
    public void EndGame()
    {
        endGame = true;
    }
}
