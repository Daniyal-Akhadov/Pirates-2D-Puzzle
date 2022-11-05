using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Utilities.TimeManagement
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private List<TimerData> _timers;

        public void SetTimer(int index)
        {
            var timer = _timers[index];
            StartCoroutine(StartTimer(timer));
        }

        private IEnumerator StartTimer(TimerData timerData)
        {
            yield return new WaitForSeconds(timerData.Delay);
            timerData.TimesUp?.Invoke();
        }

        [Serializable]
        public class TimerData
        {
            public float Delay;
            public UnityEvent TimesUp;
        }
    }
}