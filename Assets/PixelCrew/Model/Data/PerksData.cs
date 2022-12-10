using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringProperty _used = new();
        [SerializeField] private List<string> _unlocked;

        public StringProperty Used => _used;

        public void AddPerk(string id)
        {
            if (IsUnlocked(id) == false)
            {
                _unlocked.Add(id);
            }
        }

        public bool IsUnlocked(string id)
        {
            return _unlocked.Contains(id);
        }
    }
}