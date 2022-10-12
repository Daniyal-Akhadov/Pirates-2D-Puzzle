using PixelCrew.Hero;
using UnityEngine;

namespace PixelCrew.Loot
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private int _addHealthValue = 1;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out HeroHealth health))
            {
                health.AddHealth(_addHealthValue);
                Destroy(gameObject);
            }
        }
    }
}