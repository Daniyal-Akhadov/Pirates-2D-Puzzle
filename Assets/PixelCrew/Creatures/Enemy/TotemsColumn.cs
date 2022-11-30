using System.Collections.Generic;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Utilities.TimeManagement;
using UnityEngine;

namespace PixelCrew.Creatures.Enemy
{
    public class TotemsColumn : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAI> _totems;
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private Cooldown _cooldownBetweenAttack;

        private int _currentTotemIndex;

        private void Update()
        {
            if (_totems.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
                return;
            }

            if (_totems.Count > 0 && _vision.IsTouchingLayer == true)
            {
                if (_cooldownBetweenAttack.IsReady == true)
                {
                    _currentTotemIndex %= _totems.Count;
                    var totem = _totems[_currentTotemIndex];

                    if (totem != null)
                    {
                        _totems[_currentTotemIndex++].RangeAttack();
                        _cooldownBetweenAttack.Reset();
                    }
                    else
                    {
                        _totems.Remove(totem);
                    }
                }
            }
        }
    }
}