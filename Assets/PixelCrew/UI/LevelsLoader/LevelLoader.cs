using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.LevelsLoader
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _transitionTime = 4f;

        private string _targetScene;

        private static readonly int Enabled = Animator.StringToHash("enabled");

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnAfterSceneLoad()
        {
            InitLoader();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(StartAnimation(sceneName));
        }

        private IEnumerator StartAnimation(string sceneName)
        {
            _animator.SetBool(Enabled, true);
            yield return new WaitForSeconds(_transitionTime);
            _animator.SetBool(Enabled, false);
            _targetScene = sceneName;
            Invoke(nameof(LoadTargetScene), 0.3f);
        }

        private void LoadTargetScene()
        {
            SceneManager.LoadScene(_targetScene);
        }

        private static void InitLoader()
        {
            SceneManager.LoadScene("LoadScene", LoadSceneMode.Additive);
        }
    }
}