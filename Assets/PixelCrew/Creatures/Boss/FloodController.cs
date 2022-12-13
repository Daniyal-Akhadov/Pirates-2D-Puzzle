using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class FloodController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _floodTime;

        private Coroutine _coroutine;

        private static readonly int IsFlood = Animator.StringToHash("is_flood");

        public void StartFlooding(float delay = 0f)
        {
            if (_coroutine != null)
                return;

            _coroutine = StartCoroutine(AnimateFlooding(delay));
        }

        public void StopFlooding()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _animator.SetBool(IsFlood, false);
        }
        
        private IEnumerator AnimateFlooding(float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            _animator.SetBool(IsFlood, true);
            yield return new WaitForSeconds(_floodTime);
            _animator.SetBool(IsFlood, false);
            _coroutine = null;
        }
    }
}