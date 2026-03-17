using UnityEngine;

namespace _Scripts.Character.Abstracts {
    public interface ICharacterInput {
        public bool IsSprinting { get; }
        public Vector2 Move { get; }
        public Vector2 Look { get; }

        public void Enable();
        public void Disable();
    }
}