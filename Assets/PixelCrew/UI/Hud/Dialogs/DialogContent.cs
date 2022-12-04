using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class DialogContent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dialog;
        [SerializeField] private Image _icon;

        public TMP_Text Dialog => _dialog;

        public void TrySetIcon(Sprite icon)
        {
            if (_icon != null)
                _icon.sprite = icon;
        }
    }
}