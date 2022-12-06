using System;
using UnityEngine;

namespace PixelCrew.Utilities.TimeManagement
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        private float _timesUp;

        public bool IsReady => _timesUp <= Time.time;
        public float Value => _value;

        public Cooldown()
        {
            _timesUp = 0f;
        }

        public void Reset()
        {
            _timesUp = _value + Time.time;
        }

        public void ResetTimesUp()
        {
            _timesUp = 0f;
        }
    }
}