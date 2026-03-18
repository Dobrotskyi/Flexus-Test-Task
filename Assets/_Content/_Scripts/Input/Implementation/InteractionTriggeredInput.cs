using _Scripts.Input.Abstracts;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Input.Implementation {
    public class InteractionTriggeredInput : MonoBehaviour, IInteractionTrigger, IInteractionTriggerController {
        private readonly Subject<bool> _triggered = new();
        private CharacterInputActions.InteractionActions _input;

        public Observable<bool> Triggered => _triggered;

        public void Init(CharacterInputActions.InteractionActions input) {
            _input = input;
            _input.Trigger.performed += OnTriggered;
        }

        public void Enable() {
            _input.Enable();
        }

        public void Disable() {
            _input.Disable();
        }

        private void OnTriggered(InputAction.CallbackContext ctx) {
            float input = ctx.ReadValue<float>();
            if (input == 0)
                return;
            _triggered.OnNext(input > 0);
        }

        private void OnDestroy() {
            if (_input.Trigger == null)
                return;
            _input.Trigger.performed -= OnTriggered;
        }
    }
}