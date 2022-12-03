using PixelCrew.Model.Data;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/Dialog", fileName = "Dialog")]
    public class DialogDefinition : ScriptableObject
    {
        [SerializeField] private DialogData _data;

        public DialogData DialogData => _data;
    }
}