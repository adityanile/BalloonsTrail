using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonPeggle
{
    public class TouchManager : MonoBehaviour
    {
        private Transform blower;

        // Start is called before the first frame update
        void Start()
        {
            blower = BlowerManager.instance.gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 pos = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(pos);
                        touchPos.z = 0;

                        Vector3 dir = (touchPos - blower.position).normalized;
                        blower.up = dir;

                        break;

                    case TouchPhase.Moved:

                        touchPos = Camera.main.ScreenToWorldPoint(pos);
                        touchPos.z = 0;

                        dir = (touchPos - blower.position).normalized;
                        blower.up = dir;

                        break;

                    case TouchPhase.Ended:

                        BlowerManager.instance.SprayWater();
                        break;
                }

            }
            
        }
    }
}
