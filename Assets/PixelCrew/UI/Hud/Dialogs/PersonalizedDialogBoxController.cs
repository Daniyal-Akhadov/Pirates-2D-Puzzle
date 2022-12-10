using PixelCrew.Model.Data;
using UnityEngine;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _rightContent;

        protected override DialogContent CurrentContent =>
            CurrentSentence.Side == Side.Left ? LeftContent : _rightContent;

        protected override void OnOpenComplete()
        {
            _rightContent.gameObject.SetActive(CurrentSentence.Side == Side.Right);
            LeftContent.gameObject.SetActive(CurrentSentence.Side == Side.Left);
            base.OnOpenComplete();
        }

        protected override void OnCloseComplete()
        {
            _rightContent.gameObject.SetActive(false);
            LeftContent.gameObject.SetActive(false);
            base.OnCloseComplete();
        }
    }
}