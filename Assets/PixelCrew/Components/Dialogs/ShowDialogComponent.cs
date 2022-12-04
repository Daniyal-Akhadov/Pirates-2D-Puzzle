using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Hud.Dialogs;
using UnityEngine;

namespace PixelCrew.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private DialogMode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDefinition _external;

        private DialogBoxController _dialogBox;

        private DialogData Data
        {
            get
            {
                return _mode switch
                {
                    DialogMode.Bound => _bound,
                    DialogMode.External => _external.DialogData,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindDialogBox();
            
            _dialogBox.ShowDialog(Data);
        }

        private DialogBoxController FindDialogBox()
        {
            GameObject controller = Data.Type switch
            {
                DialogType.Simple => GameObject.FindWithTag("SimpleDialog"),
                DialogType.Personalized => GameObject.FindWithTag("PersonalizedDialog"),
                _ => throw new ArgumentOutOfRangeException()
            };

            return controller.GetComponent<DialogBoxController>();
        }

        public void Show(DialogDefinition definition)
        {
            if (_dialogBox == null)
                _dialogBox = FindDialogBox();
            
            _dialogBox.ShowDialog(definition.DialogData);
        }

        public enum DialogMode
        {
            Bound,
            External
        }
    }
}