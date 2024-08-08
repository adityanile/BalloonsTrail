using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private GameObject last;

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
        collision.transform.parent = transform.parent;
        
        collision.transform.position = pivot.position;
        pivot.GetComponent<DistanceJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();

        TailManager tm = collision.gameObject.AddComponent<TailManager>();
        DistanceJoint2D joint = collision.gameObject.AddComponent<DistanceJoint2D>();

        tm.head = last ? last : gameObject;

        joint.connectedBody = tm.head.GetComponent<Rigidbody2D>();
        joint.autoConfigureDistance = false;
        joint.distance = distanceOffset;

        last = collision.gameObject;
    }


}
