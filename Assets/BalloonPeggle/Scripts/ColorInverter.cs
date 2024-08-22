using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonPeggle
{
    public class ColorInverter : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            Collider2D collider = other.GetComponent<PolygonCollider2D>();
            SpriteRenderer sp = other.GetComponent<SpriteRenderer>();

            sp.color = Color.white;
            collider.enabled = false;
        }
    }
}
