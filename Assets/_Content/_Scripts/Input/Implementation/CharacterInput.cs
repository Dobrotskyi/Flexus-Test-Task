using _Scripts.Input.Abstracts;
using UnityEngine;

namespace _Scripts.Input.Implementation {
    public class CharacterInput : MonoBehaviour, ICharacterInput, ICharacterInputController {
        private CharacterInputActions.CharacterActions _input;
        private bool _disposed;

        public Vector2 Move => _input.Move.ReadValue<Vector2>();
        public Vector2 Look => _input.Look.ReadValue<Vector2>();
        public bool IsSprinting => _input.Sprint.IsPressed();
        public ICharacterInput Input => this;
        public CharacterInputActions.CharacterActions Actions => _input;

        public void Init(CharacterInputActions.CharacterActions input) {
            _input = input;
        }

        public void Enable() {
            _input.Enable();
        }

        public void Disable() {
            _input.Disable();
        }

        public void Dispose() {
            if (_disposed)
                return;
            _input.Disable();
            _disposed = true;
        }

        private void OnDestroy() {
            Dispose();
        }
    }
}