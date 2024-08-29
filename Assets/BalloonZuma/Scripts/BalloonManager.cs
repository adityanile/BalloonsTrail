using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalloonZuma
{
    public class BalloonManager : MonoBehaviour
    {
        private LineRenderer line;

        private bool reached = false;

        [SerializeField]
        private float offset = 0.1f;
        [SerializeField]
        private float speed = 1f;

        private float raycastDistance = 0.6f;

        private Vector3 followPoint;
        private int index = 0;

        public GameObject next;
        public GameObject prev;

        void Start()
        {
            line = transform.parent.GetComponent<LineRenderer>();
            followPoint = line.GetPosition(0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (CheckMovement())
            {
                MoveBalloon(followPoint);
            }
        }

        bool CheckMovement()
        {
            if (gameObject.CompareTag("Last"))
                return true;
            else
            {
                if (IsPrevPersent(raycastDistance))
                    return true;
                else
                    return false;
            }
        }

        void MoveBalloon(Vector3 pos)
        {
            if (!reached)
            {
                Vector3 dir = (pos - transform.position);
                float dist = dir.magnitude;

                if (dist > offset)
                {
                    transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
                }
                else
                {
                    reached = true;
                    index++;

                    if (index % line.positionCount == 0)
                    {
                        // Player Lost Event Trigger
                        Debug.Log("You Lost");
                        SceneManager.LoadScene("BalloonZuma");
                    }
                }
            }
            else
            {
                followPoint = line.GetPosition(index);
                reached = false;
            }
        }


        // Check if it have a balloon prev to it or its destroyed
        bool IsPrevPersent(float distance)
        {
            Vector3 dir = (prev.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance);

            if (hit.collider)
            {
                if (hit.collider.gameObject.Equals(prev))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void DestroyBalloon()
        {

            if (prev)
            {
                BalloonManager bPrev = prev.GetComponent<BalloonManager>();

                if (next)
                    bPrev.next = next;
                else
                    bPrev.next = null;
            }

            if (next)
            {
                BalloonManager bNext = next.GetComponent<BalloonManager>();

                if (prev)
                    bNext.prev = prev;
                else
                    bNext.prev = null;
            }
            Destroy(gameObject);
        }
    }
}
