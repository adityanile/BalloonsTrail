using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailManager : MonoBehaviour
{
    public GameObject head;

    [SerializeField]
    private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
        transform.up = (head.transform.position - transform.position).normalized;
    }
}
