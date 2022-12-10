using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components.Input
{
    public class PlayerInputControlComponent : MonoBehaviour
    {
        private HeroInputReader _input;

        private void Start()
        {
            _input = FindObjectOfType<HeroInputReader>();
        }

        public void Allow()
        {
            _input.AllowInput();
        }

        public void Block()
        {
            _input.BlockInput();
        }
    }
}