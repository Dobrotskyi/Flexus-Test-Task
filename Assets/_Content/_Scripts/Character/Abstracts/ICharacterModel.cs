using UnityEngine;

namespace _Scripts.Character.Abstracts {
    public interface ICharacterModel {
        public Transform Transform { get; }
        public Transform CameraTarget { get; }
        public CharacterController Controller { get; }

        public void SetAnimationParameters(ICharacterAnimationParameters parameters);
    }
}