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
        if (collision.CompareTag("Middle") || collision.CompareTag("Last"))
        {
            HeadManager hm = collision.transform.parent.GetChild(0).GetComponent<HeadManager>();
            hm.DestroyLastBalloon();
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            HeadManager hm = collision.GetComponent<HeadManager>();
            hm.DestroyLastBalloon();
            Destroy(gameObject);
        }

        if (collision.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
