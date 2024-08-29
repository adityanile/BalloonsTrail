using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonZuma
{
    public class BalloonSpawner : MonoBehaviour
    {
        public List<GameObject> balloons;

        public Transform parent;

        [SerializeField]
        private float spawnRate = 0.5f;

        public GameObject lastSpawned;
        public GameObject currentSpawned;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("SpawnABalloon", 0, spawnRate);
        }

        void SpawnABalloon()
        {
            int index = Random.Range(0, balloons.Count);
            currentSpawned = Instantiate(balloons[index], transform.position, Quaternion.identity, parent);
            currentSpawned.tag = "Last";

            // Linking of Balloons to one another
            if(lastSpawned != null )
            {
                lastSpawned.tag = "Untagged";

                BalloonManager last = lastSpawned.GetComponent<BalloonManager>();
                BalloonManager curr = currentSpawned.GetComponent<BalloonManager>();

                last.prev = currentSpawned;
                curr.next = lastSpawned;
            }

            lastSpawned = currentSpawned;
        }

    }
}
