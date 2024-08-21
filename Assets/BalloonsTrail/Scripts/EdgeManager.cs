using UnityEngine;

namespace BalloonTrail
{
    public class EdgeManager : MonoBehaviour
    {
        public GameObject spawnSide;

        public TouchManager touchManager;

        [SerializeField]
        bool vertical = true;

        public Transform parent;

        private static GameObject lastSnake;
        private static GameObject currentSnake;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                lastSnake = collision.transform.parent.gameObject;
                currentSnake = Instantiate(lastSnake, Vector3.zero, lastSnake.transform.rotation, parent);

                HeadManager head = currentSnake.transform.GetChild(0).GetComponent<HeadManager>();

                foreach (var i in head.parts)
                {
                    Vector2 pos;

                    if (vertical)
                        pos = new Vector2(i.transform.position.x + spawnSide.transform.position.x, i.transform.position.y);
                    else
                        pos = new Vector2(i.transform.position.x, i.transform.position.y + spawnSide.transform.position.y);

                    i.SetActive(false);
                    i.transform.position = pos;
                    i.SetActive(true);
                }
                touchManager.player = head;

                // Registering new snake
                SnakesManager.instance.AddASnake(head);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Last"))
            {
                if (collision.transform.parent.gameObject != currentSnake)
                {
                    HeadManager hm = collision.transform.parent.GetChild(0).GetComponent<HeadManager>();
                    SnakesManager.instance.RemoveASnake(hm);
                    Destroy(collision.transform.parent.gameObject);
                }

            }
        }
    }
}