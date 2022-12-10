using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class LerpAnimatedUtils
    {
        public static Coroutine LerpAnimated(this MonoBehaviour behaviour, float start, float end, float time,
            Action<float> onFrame)
        {
            return behaviour.StartCoroutine(Animate(start, end, time, onFrame));
        }

        private static IEnumerator Animate(float start, float end, float animationTime, Action<float> onFrame)
        {
            float timer = 0f;
            onFrame(start);

            while (timer <= animationTime)
            {
                timer += Time.deltaTime;
                var temp = Mathf.MoveTowards(start, end, timer / animationTime);
                onFrame(temp);
                yield return null;
            }

            onFrame(end);
        }
    }
}