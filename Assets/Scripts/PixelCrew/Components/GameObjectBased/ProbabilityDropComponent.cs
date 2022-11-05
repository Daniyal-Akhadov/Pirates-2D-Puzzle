using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.GameObjectBased
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private DropData[] _drops;
        [SerializeField] private UnityEvent<GameObject[]> _dropCalculated;

        private float _totalPercentProbability;
        private const int MaxPercent = 100;

        private void Awake()
        {
            _totalPercentProbability = _drops.Sum(data => data.PercentProbability);
        }

        public void CalculateDrop()
        {
            int currentItemIndex = 0;
            var itemsToDrop = new GameObject[_count];
            var sortedDrop = _drops.OrderBy(data => data.PercentProbability);

            while (currentItemIndex < _count)
            {
                float random = UnityEngine.Random.value * _totalPercentProbability; // maybe need to change row

                foreach (var data in sortedDrop)
                {
                    if (data.PercentProbability / MaxPercent >= random)
                    {
                        itemsToDrop[currentItemIndex++] = data.Drop;
                        break;
                    }
                }
            }

            _dropCalculated?.Invoke(itemsToDrop);
        }

        [Serializable]
        private class DropData
        {
            public GameObject Drop;
            [Range(0, 100)] public float PercentProbability;
        }
    }
}