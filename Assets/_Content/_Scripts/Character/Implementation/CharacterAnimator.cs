using _Scripts.Character.Abstracts;
using AYellowpaper;
using UnityEngine;

namespace _Scripts.Character.Implementation {
    public class CharacterAnimator : MonoBehaviour {
        [SerializeField] private Animator _animator;
        [SerializeField] private InterfaceReference<ICharacterAnimationParameters> _parametersRef;
        private AnimatorValueMapping[] _mappings;

        private ICharacterAnimationParameters _parameters {
            get => _parametersRef.Value;
            set => _parametersRef.Value = value;
        }

        private void Awake() {
            CreateMappings();
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
            if (_parameters == null)
                _parameters = GetComponentInChildren<ICharacterAnimationParameters>();
            if (_parameters == null)
                Debug.LogWarning(
                    $"Couldn`t find an {nameof(ICharacterAnimationParameters)} component on {gameObject.name} and it children");
        }
#endif
    }
}