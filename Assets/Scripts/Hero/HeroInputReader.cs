using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew
{
    [RequireComponent(typeof(HeroMovement))]
    [RequireComponent(typeof(HeroJumper))]
    [RequireComponent(typeof(HeroInteract))]
    public class HeroInputReader : MonoBehaviour
    {
        private HeroMovement _movement;
        private HeroJumper _jumper;
        private HeroInteract _interact;
        private HeroInputAction _input;

        private void Awake()
        {
            _movement = GetComponent<HeroMovement>();
            _jumper = GetComponent<HeroJumper>();
            _interact = GetComponent<HeroInteract>();
            _input = new HeroInputAction();

            _input.Hero.Movement.performed += OnMovement;
            _input.Hero.Movement.canceled += OnMovement;

            _input.Hero.Jump.performed += OnJump;
            _input.Hero.Jump.canceled += OnJump;

            _input.Hero.Interact.started += OnInteract;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<float>();
            _movement.SetDirection(direction);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            bool pressed = context.performed == true;
            _jumper.Jump(pressed);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            _interact.Interact();
        }
    }
}