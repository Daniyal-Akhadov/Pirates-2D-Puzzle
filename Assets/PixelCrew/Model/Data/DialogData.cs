using System;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private string[] _sentences;
        
        public string[] Sentences => _sentences;
    }
}