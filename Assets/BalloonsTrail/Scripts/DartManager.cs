using System.Collections;
using UnityEngine;

public class DartManager : MonoBehaviour
{
    private Vector3 landingPos;

    [SerializeField]
    private float offset = 0.1f;
    [SerializeField]
    private float speed = 1.5f;

    private bool reached = false;

    [SerializeField]
    private float selfDestructionTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        landingPos = BalloonSpawner.GetSpawnPos();
        transform.position = (landingPos - transform.position);

        StartCoroutine(SelfDestruction());
    }

    // Update is called once per frame
    void Update()
    {
        if (!reached)
        {
            Vector3 dir = (landingPos - transform.position);
            float distance = dir.magnitude;

            if (distance > offset)
            {
                transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
                transform.up = dir.normalized;
            }
            else
            {
                reached = true;
            }
        }
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
    }

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(selfDestructionTime);
        Destroy(gameObject);
    }
}
