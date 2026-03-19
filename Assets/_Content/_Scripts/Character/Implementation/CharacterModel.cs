using _Scripts.Character.Abstracts;
using UnityEngine;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    public class CharacterModel : MonoBehaviour {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private Renderer _renderer;

        public Transform Transform => transform != null ? transform : null;
        [field: SerializeField] public Transform CameraTarget { private set; get; }
        [field: SerializeField] public Controller Controller { private set; get; }

        public bool IsActive => Controller.enabled;

        public void SetActive(bool visible) {
            _renderer.gameObject.SetActive(visible);
            Controller.enabled = visible;
        }

        public void SetAnimationParameters(ICharacterAnimationParameters parameters) {
            _animator.Init(parameters);
        }
    }
}