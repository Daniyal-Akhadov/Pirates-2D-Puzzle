﻿using UnityEngine;

namespace PixelCrew.Utilities
{
    public static class SpawnUtils
    {
        private const string ContainerName = "###SPAWNED###";

        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            var container = GameObject.Find(ContainerName);

            if (container == null)
                container = new GameObject(ContainerName);

            var result = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity,
                container.transform
            );

            return result;
        }
    }
}