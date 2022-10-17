using System;
using PixelCrew.Loot;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Hero
{
    public class HeroCoinScore : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _coinExplosion;
        [SerializeField] private UnityEvent _coinExplosionCompleted;

        private int _currentScore;

        private const int MaxCoinToDispose = 5;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Coin coin))
            {
                TakeCoin(coin);
            }
        }

        private void TakeCoin(Coin coin)
        {
            int profit = coin.Profit;

            if (profit <= 0)
            {
                throw new ArgumentException($"Coin profit equals {coin}. It's <= 0!");
            }

            _currentScore += profit;
            print(_currentScore);
        }

        public void TrySpawnCoinExplosion()
        {
            if (_currentScore > 0)
            {
                int coinsToDisposeNumber = Mathf.Min(_currentScore, MaxCoinToDispose);
                _currentScore -= coinsToDisposeNumber;

                var burst = _coinExplosion.emission.GetBurst(0);
                burst.count = coinsToDisposeNumber;
                _coinExplosion.emission.SetBurst(0, burst);

                _coinExplosion.Play();
                _coinExplosionCompleted?.Invoke();
                print(_currentScore);
            }
        }

        public void AddCoinScore(int profit)
        {
            if (profit <= 0)
            {
                throw new ArgumentException($"Coin profit equals {profit}. It's <= 0!");
            }

            _currentScore += profit;
            print(_currentScore);
        }
    }
}