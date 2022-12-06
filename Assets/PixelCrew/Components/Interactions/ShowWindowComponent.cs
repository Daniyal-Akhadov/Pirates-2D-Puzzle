using PixelCrew.Utilities;
using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField] private string _path;

        public void Show()
        {
            WindowUtils.CreateWindow(_path);
        }
    }
}