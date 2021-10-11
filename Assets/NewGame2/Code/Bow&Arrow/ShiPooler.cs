using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiPooler : MonoBehaviour
{
    public GameObject [] path;
    public float spawnRate;
    public ObjectPooler pooler;

    private Vector3 faceToEnd;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySpawn(Random.Range(0, 3)));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnRate)
        {
            time = 0.0f;
            StartCoroutine(DelaySpawn(Random.Range(0, 3)));
        }
    }

    public void SpawnShip()
    {
        int numOfChosen = Random.Range(0, path.Length);
        GameObject chosenPath = path[numOfChosen];
        
        if (numOfChosen == 0)
        {
            faceToEnd =  new Vector3(0, -90, 0);
        }
        else
        {
            faceToEnd = new Vector3(0, 90, 0);
        }

        pooler.SpawnFromPool("Ship", chosenPath.transform, new Vector3(0, 0, 0), faceToEnd);

    }

    IEnumerator DelaySpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnShip();
    }
}
