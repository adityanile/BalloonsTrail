using System.Collections;
using UnityEngine;

public class DartManager : MonoBehaviour
{
    private Vector3 landingPos;

    [SerializeField]
    private float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        landingPos = BalloonSpawner.GetSpawnPos();
        transform.up = (landingPos - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounds"))
        {
            Destroy(gameObject);
            return;
        }  

        if (collision.CompareTag("Middle"))
        {
            // Get which balloon to destroy
            HeadManager hm = collision.transform.parent.GetChild(0).GetComponent<HeadManager>();
            int hitIndex = hm.parts.FindIndex(p => p.gameObject.Equals(collision.gameObject));

            foreach (var head in SnakesManager.instance.allHeads)
            {
                if (head != null)
                    head.DestroyMentionedBalloon(hitIndex);
            }

            // Also Destroy the dart itself
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player") || collision.CompareTag("Last"))
        {
            foreach(var head in SnakesManager.instance.allHeads)
            {
                if(head != null)
                    head.DestroyLastBalloon();
            }

            // Also Destroy the dart itself
            Destroy(gameObject);
        }
    }
}
