using _Scripts.Input.Abstracts;
using UnityEngine;

namespace _Scripts.Input.Implementation {
    public class CharacterInput : MonoBehaviour, ICharacterInput, ICharacterInputController {
        private CharacterInputActions _input;
        private bool _disposed;

        public Vector2 Move => _input.Character.Move.ReadValue<Vector2>();
        public Vector2 Look => _input.Character.Look.ReadValue<Vector2>();
        public bool IsSprinting => _input.Character.Sprint.IsPressed();
        public ICharacterInput Input => this;

        public void Init() {
            if (_input != null)
                throw new System.Exception($"{nameof(CharacterInput)} was already initialized");
            _input = new CharacterInputActions();
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
            _input?.Disable();
            _input?.Dispose();
            _disposed = true;
        }

        private void OnDestroy() {
            Dispose();
        }
    }
}