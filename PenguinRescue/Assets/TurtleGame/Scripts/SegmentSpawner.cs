using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject segment;
    public float speed = 3f;
    GameObject lastObject;

    public void SpawnTile()
    {        
        if(lastObject != null)
        {
            GameObject temp = Instantiate(segment, lastObject.transform.GetChild(0).transform.position, Quaternion.identity);
            lastObject = temp;
        }
        else
        {
            GameObject temp = Instantiate(segment, Vector3.zero, Quaternion.identity);
            lastObject = temp;
        }
    }
    private void Update()
    {
        speed += Time.deltaTime * 0.5f;
    }
    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnTile();
        }
    }
}
