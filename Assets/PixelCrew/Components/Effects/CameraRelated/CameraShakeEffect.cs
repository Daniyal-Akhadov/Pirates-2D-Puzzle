using System.Collections;
using Cinemachine;
using UnityEngine;

namespace PixelCrew.Components.Effects.CameraRelated
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShakeEffect : MonoBehaviour
    {
        [SerializeField] private float _timeToShake = 0.5f;
        [SerializeField] private float _amplitude = 3f;
        [SerializeField] private float _frequency = 3.2f;

        private CinemachineBasicMultiChannelPerlin _cameraNoise;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            var _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_timeToShake);
        }

        public void TryShake()
        {
            if (_coroutine != null)
                return;

            _coroutine = StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            _cameraNoise.m_FrequencyGain = _frequency;
            _cameraNoise.m_AmplitudeGain = _amplitude;

            yield return _waitForSeconds;

            _cameraNoise.m_FrequencyGain = 0f;
            _cameraNoise.m_AmplitudeGain = 0f;
            _coroutine = null;
        }
    }
}