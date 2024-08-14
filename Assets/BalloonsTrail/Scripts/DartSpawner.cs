using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawner : MonoBehaviour
{
    public GameObject dart;

    [SerializeField]
    public List<Transform> spawnPos;

    [SerializeField]
    private float startAfter = 10f;
    [SerializeField]
    private float interval = 30f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnADart", startAfter, interval);
    }

    void SpawnADart()
    {
        int spawnPosIndex = Random.Range(0, spawnPos.Count);
        Instantiate(dart, spawnPos[spawnPosIndex].position, Quaternion.identity, transform);
    }
}
