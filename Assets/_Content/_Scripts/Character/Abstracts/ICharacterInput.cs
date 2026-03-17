using R3;
using UnityEngine;

namespace _Scripts.Character.Abstracts {
    public interface ICharacterInput {
        public Vector2 Move { get; }
        public Vector2 Look { get; }

        public void Enable();
        public void Disable();
    }
}