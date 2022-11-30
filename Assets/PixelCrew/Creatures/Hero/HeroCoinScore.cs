using PixelCrew.Components.GameObjectBased;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Hero
{
    public class HeroCoinScore : MonoBehaviour
    {
        [SerializeField] private ProbabilityDropComponent _dropCoins;
        [SerializeField] private UnityEvent _coinExplosionCompleted;

        private GameSession _session;

        private int CoinCount => _session.Data.Inventory.Count("Coin");

        private const int MaxCoinToDispose = 7 ;

        public void Init(GameSession session)
        {
            _session = session;
        }

        public void TrySpawnCoinExplosion()
        {
            int coinsCount = CoinCount;

            if (coinsCount > 0)
            {
                int coinsToDisposeNumber = Mathf.Min(coinsCount, MaxCoinToDispose);
                _session.Data.Inventory.Remove("Coin", coinsToDisposeNumber);
                _dropCoins.SetCount(coinsToDisposeNumber);
                _dropCoins.CalculateDrop();
                _coinExplosionCompleted?.Invoke();
            }
        }
    }
}