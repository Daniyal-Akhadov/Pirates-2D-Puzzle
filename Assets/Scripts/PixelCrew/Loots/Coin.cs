using UnityEngine;

namespace PixelCrew.Loots
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _profit;

        public int Profit => _profit;
    }
}