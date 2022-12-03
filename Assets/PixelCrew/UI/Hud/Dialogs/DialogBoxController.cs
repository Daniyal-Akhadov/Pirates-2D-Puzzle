using System;
using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.Utilities;
using TMPro;
using UnityEngine;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dialog;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space] [SerializeField] private float _typeCooldown = 0.1f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogData _dialogData;
        private int _currentSentence;
        private AudioSource _source;

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");
        private Coroutine _typingCoroutine;

        private void Start()
        {
            _source = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData dialogData)
        {
            _dialogData = dialogData;
            _currentSentence = 0;
            _dialog.text = string.Empty;

            _container.SetActive(true);
            _source.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }

        public void OnSkip()
        {
            if (_typingCoroutine == null) return;
            StopTypingAnimation();
            _dialog.text = _dialogData.Sentences[_currentSentence];
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
        }

        private void StopTypingAnimation()
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }

        private void OnOpenComplete()
        {
            _typingCoroutine = StartCoroutine(TypeDialog());
        }

        private void OnCloseComplete()
        {
            _container.SetActive(false);
        }

        private IEnumerator TypeDialog()
        {
            _dialog.text = string.Empty;
            var sentence = _dialogData.Sentences[_currentSentence];

            foreach (var letter in sentence)
            {
                _dialog.text += letter;
                _source.PlayOneShot(_typing);
                yield return new WaitForSeconds(_typeCooldown);
            }

            _typingCoroutine = null;
        }
    }
}