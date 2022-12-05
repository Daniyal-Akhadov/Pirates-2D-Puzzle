using System;
using Cinemachine;
using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components.LevelManagement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private void Start()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = FindObjectOfType<Hero>().transform;
        }
    }
}