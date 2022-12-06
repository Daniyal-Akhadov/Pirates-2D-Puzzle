using PixelCrew.Creatures.Core;
using PixelCrew.Model.Definitions;
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
        private Hero _hero;

        private void Awake()
        {
            _input = new HeroInputAction();

            _input.Hero.Movement.performed += OnMovement;
            _input.Hero.Movement.canceled += OnMovement;

            _input.Hero.Jump.performed += OnJump;
            _input.Hero.Jump.canceled += OnJump;

            _input.Hero.Jump.started += OnPressJump;

            _input.Hero.Interact.started += OnInteract;

            _input.Hero.Attack.started += OnAttack;

            _input.Hero.QueueThrow.performed += OnQueueThrow;

            _input.Hero.NextItem.started += OnNextItem;

            _input.Hero.UseItem.started += OnUseItem;

            _input.Hero.UsePerk.started += OnUsePerk;
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
            HeroAttacker attacker, ProjectileThrower thrower, Hero hero)
        {
            _movementComponent = movementComponent;
            _jumper = jumper;
            _interact = interact;
            _attacker = attacker;
            _thrower = thrower;
            _hero = hero;
        }

        public void AllowInput()
        {
            gameObject.SetActive(true);
        }

        public void BlockInput()
        {
            gameObject.SetActive(false);
        }

        private void OnUseItem(InputAction.CallbackContext context)
        {
            _hero.OnUseItem();
        }

        private void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.started)
                _hero.NextItem();
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

        private void OnPressJump(InputAction.CallbackContext context)
        {
            if (context.started == true)
                _jumper.Press();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
                _interact.Interact();
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
                _attacker.Attack();
        }

        private void OnQueueThrow(InputAction.CallbackContext context)
        {
            StartCoroutine(_thrower.TryThrowQueue());
        }

        private void OnUsePerk(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                print("Yes, I start use shield");
                _hero.OnUsePerk();
            }
        }
    }
}