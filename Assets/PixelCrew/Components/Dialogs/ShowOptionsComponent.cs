using PixelCrew.UI.Hud.Dialogs;
using UnityEngine;

namespace PixelCrew.Components.Dialogs
{
    public class ShowOptionsComponent : MonoBehaviour
    {
        [SerializeField] private OptionDialogData _dialogData;

        private OptionDialogController _dialogController;
        
        public void Show()
        {
            if (_dialogController == null)
                _dialogController = FindObjectOfType<OptionDialogController>();

            _dialogController.Show(_dialogData);
        }
    }
}