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

    // When balloon collides with the head of the snake
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bounds"))
        {
            collision.transform.parent = transform.parent;
            
            collision.tag = "Last";
            parts[parts.Count - 2].tag = "Middle";

            collision.transform.position = pivot.position;
            pivot.GetComponent<DistanceJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();

            TailManager tm = collision.gameObject.AddComponent<TailManager>();
            DistanceJoint2D joint = collision.gameObject.AddComponent<DistanceJoint2D>();

            tm.head = parts[parts.Count - 2];

            joint.connectedBody = tm.head.GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
            joint.distance = distanceOffset;

            // Swap the pivot to the last element
            parts.RemoveAt(parts.Count - 1);
            parts.Add(collision.gameObject);
            parts.Add(pivot.gameObject);
        }
    }


}
