using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        private MovementComponent _movementComponent;
        private HeroJumper _jumper;
        private HeroInteract _interact;
        private HeroAttacker _attacker;
        private HeroInputAction _input;
        private ProjectileThrower _thrower;

        private void Awake()
        {
            _input = new HeroInputAction();

            _input.Hero.Movement.performed += OnMovement;
            _input.Hero.Movement.canceled += OnMovement;

            _input.Hero.Jump.performed += OnJump;
            _input.Hero.Jump.canceled += OnJump;

            _input.Hero.Interact.started += OnInteract;

            _input.Hero.Attack.started += OnAttack;

            _input.Hero.Throw.started += OnThrow;
            _input.Hero.QueueThrow.performed += OnQueueThrow;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        public void Init(MovementComponent movementComponent,
            HeroJumper jumper, HeroInteract interact,
            HeroAttacker attacker, ProjectileThrower thrower)
        {
            _movementComponent = movementComponent;
            _jumper = jumper;
            _interact = interact;
            _attacker = attacker;
            _thrower = thrower;
        }

        public void AllowInput()
        {
            gameObject.SetActive(true);
        }

        public void BlockInput()
        {
            gameObject.SetActive(false);
        }
        
        private void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<float>();
            _movementComponent.SetDirection(direction);
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

        private void OnAttack(InputAction.CallbackContext context)
        {
            _attacker.Attack();
        }

        private void OnThrow(InputAction.CallbackContext context)
        {
            _thrower.TryThrow();
        }

        private void OnQueueThrow(InputAction.CallbackContext context)
        {
            StartCoroutine(_thrower.TryThrowQueue());
        }
    }
}