using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    [SerializeField]
    private bool isAdded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isAdded)
            {
                int len = SnakesManager.instance.allHeads.Count;
                GameObject[] balloons = new GameObject[len];
                balloons[0] = gameObject;

                for (int i = 1; i < len; i++)
                    balloons[i] = Instantiate(gameObject);

                for (int i = 0; i < len; i++)
                {
                    if(SnakesManager.instance.allHeads[i] != null)
                    SnakesManager.instance.allHeads[i].AddBalloon(balloons[i]);
                }

                isAdded = true;
            }
            else
            {
                foreach (HeadManager head in SnakesManager.instance.allHeads)
                {
                    if(head != null)
                    head.DestroyLastBalloon();
                }
            }
        }
    }
}
