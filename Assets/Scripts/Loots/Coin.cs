using UnityEngine;

namespace PixelCrew.Loot
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _profit;

        public int Profit => _profit;
    }
}