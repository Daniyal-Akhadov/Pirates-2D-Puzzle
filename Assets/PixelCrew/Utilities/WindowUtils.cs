using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath, string parentTag = "MainUICanvas")
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.FindWithTag(parentTag);
            Object.Instantiate(window, canvas.transform);
        }
    }
}