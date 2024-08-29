using UnityEngine;

namespace BalloonPeggle
{
    public class CollisionManager : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            Destroy(other);
        }
    }
}
