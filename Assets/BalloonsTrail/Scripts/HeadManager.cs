using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    public List<GameObject> parts = new List<GameObject>();

    [SerializeField]
    private float distanceOffset = 1f;

    public Transform pivot;

    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloons"))
        {
            AddBalloon(collision.gameObject);
        }
        else if(collision.CompareTag("Middle") || collision.CompareTag("Last"))
        {
            DestroyLastBalloon();
        }
    }

    void DestroyLastBalloon()
    {
        GameObject balloon = parts[parts.Count - 2];
        Destroy(balloon);
        parts.RemoveAt(parts.Count - 2);
        parts[parts.Count - 2].tag = "Last";
    }

    void AddBalloon(GameObject balloon)
    {
        balloon.transform.parent = transform.parent;

        balloon.tag = "Last";
        parts[parts.Count - 2].tag = "Middle";

        balloon.transform.position = pivot.position;
        pivot.GetComponent<DistanceJoint2D>().connectedBody = balloon.GetComponent<Rigidbody2D>();

        TailManager tm = balloon.gameObject.AddComponent<TailManager>();
        DistanceJoint2D joint = balloon.gameObject.AddComponent<DistanceJoint2D>();

        tm.head = parts[parts.Count - 2];

        joint.connectedBody = tm.head.GetComponent<Rigidbody2D>();
        joint.autoConfigureDistance = false;
        joint.distance = distanceOffset;

        // Swap the pivot to the last element
        parts.RemoveAt(parts.Count - 1);
        parts.Add(balloon.gameObject);
        parts.Add(pivot.gameObject);
    }

}
