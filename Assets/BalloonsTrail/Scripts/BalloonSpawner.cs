using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public List<GameObject> Spawns = new List<GameObject>();

    public Transform freeBalloons;

    [SerializeField]
    private float upperLimit = 2.1f;
    [SerializeField]
    private float lowerLimit = 5f;

    [SerializeField]
    private float startAfter = 1f;

    [SerializeField]
    private float interval = 0.5f;

    private void Start()
    {
        InvokeRepeating("SpawnABalloon", startAfter, interval);
    }

    void SpawnABalloon()
    {
        int spawnIndex = Random.Range(0, Spawns.Count);
        Vector3 spawnPos = GetSpawnPos();

        Instantiate(Spawns[spawnIndex], spawnPos, Quaternion.identity, freeBalloons);
    }

    private Vector3 GetSpawnPos()
    {
        float xPos = Random.Range(-lowerLimit,lowerLimit);
        float yPos = Random.Range(-upperLimit,upperLimit);

        return new(xPos, yPos, 0);
    }

}
