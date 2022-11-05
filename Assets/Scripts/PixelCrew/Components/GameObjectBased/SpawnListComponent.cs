using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private List<SpawnComponentData> _spawners;

        public void Spawn(string id)
        {
            var result = _spawners.FirstOrDefault(element => element.Id == id);
            result?.Component.Spawn();
        }

        [Serializable]
        public class SpawnComponentData
        { 
            public string Id;
            public SpawnComponent Component;
        }
    }
}