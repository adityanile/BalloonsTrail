using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;

    public HeadManager player;
    
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
                            player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90f);
                        }
                        else
                        {   //Left swipe
                            player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
                        }
                    }
                    else
                    {   
                        if (lp.y > fp.y)
                        {   
                            //Up swipe
                            player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0f);
                        }
                        else
                        {   
                            //Down swipe
                            player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180f);
                        }
                    }
                }
            }
        }
    }
}
