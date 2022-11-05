using System;
using PixelCrew.Loots;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creatures.Hero
{
    public class HeroCoinScore : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _coinExplosion;
        [SerializeField] private UnityEvent _coinExplosionCompleted;

        private GameSession _session;

        private const int MaxCoinToDispose = 5;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Coin coin))
            {
                TakeCoin(coin);
            }
        }

        public void Init(GameSession session)
        {
            _session = session;
        }

        public void TrySpawnCoinExplosion()
        {
            if (_session.Data.Coins > 0)
            {
                int coinsToDisposeNumber = Mathf.Min(_session.Data.Coins, MaxCoinToDispose);
                _session.Data.Coins -= coinsToDisposeNumber;

                var burst = _coinExplosion.emission.GetBurst(0);
                burst.count = coinsToDisposeNumber;
                _coinExplosion.emission.SetBurst(0, burst);

                _coinExplosion.Play();
                _coinExplosionCompleted?.Invoke();
                print(_session.Data.Coins);
            }
        }

        public void AddCoinScore(int profit)
        {
            if (profit <= 0)
            {
                throw new ArgumentException($"Coin profit equals {profit}. It's <= 0!");
            }

            _session.Data.Coins += profit;
            print(_session.Data.Coins);
        }

        private void TakeCoin(Coin coin)
        {
            int profit = coin.Profit;

            if (profit <= 0)
            {
                throw new ArgumentException($"Coin profit equals {coin}. It's <= 0!");
            }

            _session.Data.Coins += profit;
            print(_session.Data.Coins);
        }
    }
}