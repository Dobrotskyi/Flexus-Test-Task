using _Scripts.Character.Abstracts;
using UnityEngine;

namespace _Scripts.Character.Implementation {
    public class CharacterAnimator : MonoBehaviour {
        [SerializeField] private Animator _animator;
        private AnimatorValueMapping[] _mappings;

        private ICharacterAnimationParameters _parameters;

        public void Init(ICharacterAnimationParameters parameters) {
            _parameters = parameters;
            CreateMappings();
            enabled = true;
        }

        private void OnEnable() {
            if (_parameters == null)
                enabled = false;
        }

        private void Update() {
            for (int i = 0; i < _mappings.Length; i++)
                _mappings[i].Check();
        }

        private void CreateMappings() {
            _mappings = new AnimatorValueMapping[] {
                new BoolValueMapping(_animator, "Sprinting", false, () => _parameters.IsSprinting),
                new FloatValueMapping(_animator, "HorizontalSpeed", 0, () => _parameters.Speed)
            };
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (_animator == null)
                _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
                Debug.LogWarning($"Couldn`t find an animator component on {gameObject.name} and it children");
        }
#endif
    }
}