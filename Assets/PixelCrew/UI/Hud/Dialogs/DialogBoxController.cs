using System;
using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space] [SerializeField] private float _typeCooldown = 0.1f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        [Space] [SerializeField] protected DialogContent LeftContent;

        private DialogData _dialogData;
        private int _currentSentence;
        private AudioSource _source;
        private Coroutine _typingCoroutine;

        protected virtual DialogContent CurrentContent => LeftContent;
        protected Sentence CurrentSentence => _dialogData.Sentences[_currentSentence];

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private UnityEvent _callback;

        private void Start()
        {
            _source = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData dialogData, UnityEvent callback = null)
        {
            _dialogData = dialogData;
            _currentSentence = 0;
            CurrentContent.Dialog.text = string.Empty;

            _container.SetActive(true);
            _source.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);

            if (callback != null)
                _callback = callback;
        }

        public void OnSkip()
        {
            if (_typingCoroutine == null) return;
            StopTypingAnimation();
            CurrentContent.Dialog.text =
                LocalizationManager.Instance.GetLocalization(_dialogData.Sentences[_currentSentence].Value);
        }

        public void OnContinue()
        {
            if (_typingCoroutine != null)
                StopTypingAnimation();

            _currentSentence++;

            var isDialogCompleted = _currentSentence >= _dialogData.Sentences.Length;

            if (isDialogCompleted == true)
            {
                HideDialogBox();
            }
            else
            {
                OnOpenComplete();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen, false);
            _source.PlayOneShot(_close);
            _callback?.Invoke();
        }

        private void StopTypingAnimation()
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }

        protected virtual void OnOpenComplete()
        {
            _typingCoroutine = StartCoroutine(TypeDialog());
        }

        protected virtual void OnCloseComplete()
        {
            _container.SetActive(false);
        }

        private IEnumerator TypeDialog()
        {
            CurrentContent.Dialog.text = string.Empty;
            var sentence = CurrentSentence;
            CurrentContent.TrySetIcon(sentence.Icon);
            var targetText = LocalizationManager.Instance.GetLocalization(sentence.Value);

            foreach (var letter in targetText)
            {
                CurrentContent.Dialog.text += letter;
                _source.PlayOneShot(_typing);
                yield return new WaitForSeconds(_typeCooldown);
            }

            _typingCoroutine = null;
        }
    }
}