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
                HeadManager[] heads = FindObjectsOfType<HeadManager>();

                GameObject[] balloons = new GameObject[heads.Length];
                balloons[0] = gameObject;

                for (int i = 1; i < heads.Length; i++)
                    balloons[i] = Instantiate(gameObject);

                for (int i = 0; i < heads.Length; i++)
                {
                    heads[i].AddBalloon(balloons[i]);
                }

                isAdded = true;
            }
            else
            {
                HeadManager[] heads = FindObjectsOfType<HeadManager>();

                foreach (HeadManager head in heads)
                {
                    head.DestroyLastBalloon();
                }
            }
        }
    }
}
