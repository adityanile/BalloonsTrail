using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonTrail
{
    public class SnakesManager : MonoBehaviour
    {
        [HideInInspector]
        public List<HeadManager> allHeads = new();

        [SerializeField]
        private HeadManager startingHead;

        public static SnakesManager instance;

        private void Start()
        {
            if (!instance)
            {
                instance = this;

                // Initial snake
                AddASnake(startingHead);
                return;
            }
            Destroy(gameObject);
        }

        public void AddASnake(HeadManager headManager)
        {
            allHeads.Add(headManager);
        }
        public void RemoveASnake(HeadManager head)
        {
            allHeads.Remove(head);
        }
    }
}
