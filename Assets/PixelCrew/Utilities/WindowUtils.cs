using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.FindWithTag("MainUICanvas");
            Object.Instantiate(window, canvas.transform);
        }
    }
}