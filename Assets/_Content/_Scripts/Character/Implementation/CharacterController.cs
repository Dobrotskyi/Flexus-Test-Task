using System;
using _Scripts.Character.Abstracts;
using UnityEngine;
using Zenject;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    [RequireComponent(typeof(Controller))]
    public class CharacterController : MonoBehaviour {
        private const float G = -9.81f;

        [Serializable]
        public struct Characteristics {
            public float MaxSpeed;
            public float CameraSensitivity;
        }

        [SerializeField] private Characteristics _characteristics;
        [SerializeField] private Controller _controller;
        private ICharacterInput _input;

        [Inject]
        public void Init(ICharacterInput input) {
            _input = input;
        }

        private void Update() {
            HandleMovement();
        }

        private void HandleMovement() {
            float targetSpeed = _characteristics.MaxSpeed;
            if (_input.Move == Vector2.zero)
                targetSpeed = 0;
            else
                AlignRotation();

            _controller.Move(transform.forward * targetSpeed * Time.deltaTime + new Vector3(0, G, 0));
        }

        private void AlignRotation() {
            transform.rotation = Quaternion.LookRotation(new Vector3(_input.Move.x, 0, _input.Move.y).normalized);
        }


#if UNITY_EDITOR
        private void OnValidate() {
            if (_controller == null)
                _controller = GetComponent<Controller>();
        }
#endif
    }
}