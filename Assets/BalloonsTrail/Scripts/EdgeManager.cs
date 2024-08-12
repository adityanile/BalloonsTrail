using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public GameObject spawnSide;

    public TouchManager touchManager;

    [SerializeField]
    bool vertical = true;

    private GameObject currentSnake;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentSnake = collision.transform.parent.gameObject;
            GameObject inst = Instantiate(currentSnake, Vector3.zero, currentSnake.transform.rotation);

            HeadManager head = inst.transform.GetChild(0).GetComponent<HeadManager>();

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

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Last"))
        {
            Destroy(currentSnake);
        }
    }
}
