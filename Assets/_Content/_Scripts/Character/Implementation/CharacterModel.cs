using _Scripts.Character.Abstracts;
using UnityEngine;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    public class CharacterModel : MonoBehaviour, ICharacterModel {
        [SerializeField] private CharacterAnimator _animator;

        public Transform Transform => transform != null ? transform : null;
        [field: SerializeField] public Transform CameraTarget { private set; get; }
        [field: SerializeField] public Controller Controller { private set; get; }

        public void SetAnimationParameters(ICharacterAnimationParameters parameters) {
            _animator.Init(parameters);
        }
    }
}