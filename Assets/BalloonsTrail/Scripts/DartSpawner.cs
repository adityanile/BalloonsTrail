using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawner : MonoBehaviour
{
    public GameObject dart;

    [SerializeField]
    public List<Transform> spawnArr;

    [SerializeField]
    private float startAfter = 10f;
    [SerializeField]
    private float interval = 30f;

    private static float upperLimit = 2.5f;
    private static float lowerLimit = 6f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnADart", startAfter, interval);
    }

    void SpawnADart()
    {
        int spawnPosIndex = Random.Range(0, 4);
        Vector3 spawnPos;
        
        if(spawnPosIndex == 0)
        {
            spawnPos = new Vector3(-lowerLimit, Random.Range(-upperLimit, upperLimit), 0);
        }
        else if(spawnPosIndex == 1)
        {
            spawnPos = new Vector3(lowerLimit, Random.Range(-upperLimit, upperLimit), 0);
        }
        else if (spawnPosIndex == 2)
        {
            spawnPos = new Vector3(Random.Range(-lowerLimit, lowerLimit), -upperLimit, 0);
        }
        else
        {
            spawnPos = new Vector3(Random.Range(-lowerLimit, lowerLimit), upperLimit, 0);
        }

        Instantiate(dart, spawnPos, Quaternion.identity, transform);
    }
}
