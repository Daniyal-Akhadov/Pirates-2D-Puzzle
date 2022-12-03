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

        private void Start()
        {
            _dialogBox = FindObjectOfType<DialogBoxController>();
        }

        public void Show()
        {
            _dialogBox.ShowDialog(Data);
        }
        
        public void Show(DialogDefinition definition)
        {
            _dialogBox.ShowDialog(definition.DialogData);
        }

        public enum DialogMode
        {
            Bound,
            External
        }
    }
}