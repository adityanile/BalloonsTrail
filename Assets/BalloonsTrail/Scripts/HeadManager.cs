using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    public List<GameObject> parts = new List<GameObject>();

    [SerializeField]
    private float distanceOffset = 1f;

    public Transform pivot;

    public delegate void MyDelegate();
    public static event MyDelegate OnValueChanged;

    private static Vector3 _followPos;

    public static Vector3 FollowPos
    {
        get => _followPos;
        set
        {
            _followPos = value;
            OnValueChanged?.Invoke();
        }
    }

    public bool reached = true;

    // Subscribing the event method
    private void OnEnable()
    {
        OnValueChanged += MoveSnake;
    }
    private void OnDisable()
    {
        OnValueChanged -= MoveSnake;    
    }

    // Invoke this method
    void MoveSnake()
    {
        Debug.Log("Motion Activated");
        reached = false;
    }

    void Update()
    {
        if (!reached)
        {
            Vector3 dir = (FollowPos - transform.position);
            float distance = dir.magnitude;

            if (distance > distanceOffset)
            {
                transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
                transform.up = dir.normalized;
            }
            else
            {
                reached = true;
            }
        }
        else
        {
            transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
        }
    }

    public void DestroyLastBalloon()
    {
        // Destroy the snake when no balloon and snake hit a dart
        if (parts.Count == 2)
        {
            Destroy(transform.parent.gameObject);
            SceneManager.LoadScene(0);
            return;
        }

        GameObject balloon = parts[parts.Count - 2];
        pivot.GetComponent<DistanceJoint2D>().connectedBody = parts[parts.Count - 3].GetComponent<Rigidbody2D>();
        Destroy(balloon);
        parts.RemoveAt(parts.Count - 2);

        if (parts.Count > 2)
            parts[parts.Count - 2].tag = "Last";
    }

    public void DestroyMentionedBalloon(int index)
    {
        GameObject balloon = parts[index];

        Rigidbody2D connectTo = parts[index-1].GetComponent<Rigidbody2D>();
        
        // Part Later to hit body should connect to upNext of the hit body
        DistanceJoint2D nextJoint = parts[index+1].GetComponent<DistanceJoint2D>(); 
        TailManager tailManager = parts[index+1].GetComponent<TailManager>();

        tailManager.head = connectTo.gameObject;
        nextJoint.connectedBody = connectTo;

        parts.RemoveAt(index);
        Destroy(balloon);
    }

    public void AddBalloon(GameObject balloon)
    {
        balloon.transform.parent = transform.parent;

        balloon.tag = "Last";
        
        if(parts.Count > 2)
        parts[parts.Count - 2].tag = "Middle";

        balloon.transform.position = pivot.position;
        pivot.GetComponent<DistanceJoint2D>().connectedBody = balloon.GetComponent<Rigidbody2D>();

        TailManager tm = balloon.gameObject.AddComponent<TailManager>();
        DistanceJoint2D joint = balloon.gameObject.AddComponent<DistanceJoint2D>();

        tm.head = parts[parts.Count - 2];

        joint.connectedBody = tm.head.GetComponent<Rigidbody2D>();
        joint.autoConfigureDistance = false;
        joint.distance = distanceOffset;

        // Swap the pivot to the last in the list
        parts.RemoveAt(parts.Count - 1);
        parts.Add(balloon.gameObject);
        parts.Add(pivot.gameObject);

        // Setting sibling as last in the transform list
        pivot.transform.SetAsLastSibling();
    }
}
