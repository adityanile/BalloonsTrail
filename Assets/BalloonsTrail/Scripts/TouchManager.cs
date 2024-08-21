using UnityEngine;

namespace BalloonTrail
{
    public class TouchManager : MonoBehaviour
    {
        private Vector3 fp;
        private Vector3 lp;
        private float dragDistance;

        public HeadManager player;

        public bool activate = false;
        public Quaternion startRotation;
        public Quaternion endRotation;
        public float time = 0;

        void Start()
        {
            dragDistance = Screen.height * 15 / 100;
        }

        void Update()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0); // get the touch

                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lp = touch.position;

                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {
                            if ((lp.x > fp.x))
                            {
                                //Right swipe
                                //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90f);

                                startRotation = player.transform.rotation;
                                endRotation = Quaternion.Euler(0, 0, -90);

                                activate = true;
                            }
                            else
                            {   //Left swipe
                                //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);

                                startRotation = player.transform.rotation;
                                endRotation = Quaternion.Euler(0, 0, 90);

                                activate = true;
                            }
                        }
                        else
                        {
                            if (lp.y > fp.y)
                            {
                                //Up swipe
                                //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0f);

                                startRotation = player.transform.rotation;
                                endRotation = Quaternion.Euler(0, 0, 0);

                                activate = true;
                            }
                            else
                            {
                                //Down swipe
                                //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180f);

                                startRotation = player.transform.rotation;
                                endRotation = Quaternion.Euler(0, 0, 180);

                                activate = true;
                            }
                        }
                    }
                    else
                    {
                        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                        pos.z = 0;
                        HeadManager.FollowPos = pos;
                    }
                }
            }

            if (activate)
            {
                if (time <= 1)
                {
                    Quaternion interPo = Quaternion.Slerp(startRotation, endRotation, time);
                    time += Time.fixedDeltaTime;
                    player.gameObject.transform.rotation = interPo;
                }
                else
                {
                    activate = false;
                    time = 0;
                }
            }
        }
    }
}