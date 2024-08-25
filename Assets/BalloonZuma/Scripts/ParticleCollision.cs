using System;
using UnityEngine;

namespace BalloonZuma
{
    public class ParticleCollision : MonoBehaviour
    {
        private BlowerManager blowerManager;

        private void Start()
        {
            blowerManager = transform.parent.GetComponent<BlowerManager>();
        }
        private void OnParticleCollision(GameObject other)
        {
            if (blowerManager.killActive)
            {
                blowerManager.killActive = false;
                BalloonManager bm = other.GetComponent<BalloonManager>();

                if (bm)
                {
                    bm.DestroyBalloon();
                }
            }
        }
    }
}
