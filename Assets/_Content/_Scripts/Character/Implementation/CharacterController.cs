using System;
using _Scripts.Character.Abstracts;
using _Scripts.Input.Abstracts;
using _Scripts.Input.Implementation;
using UnityEngine;
using Zenject;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    public class CharacterController : MonoBehaviour, ICharacterAnimationParameters {
        private const float G = -9.81f;

        [Serializable]
        public struct Characteristics {
            public float MaxSpeed;
            public float MaxSprintSpeed;
            public float RotationSmoothTime;
        }

        [SerializeField] private Characteristics _characteristics;
        [SerializeField] private CameraController _cameraController;
        private Controller _controller;
        private ICharacterInput _input;


        public bool IsSprinting => _input.IsSprinting;
        public float Speed => _controller.velocity.magnitude;
        public ICharacterModel Model { private set; get; }

        [Inject]
        public void SetInput(ICharacterInput input) {
            _input = input;
        }

        public void SetModel(ICharacterModel model) {
            Model = model;
            Transform parentTransform = transform.Find("Model");
            parentTransform ??= transform;
            Model.Transform.SetParent(parentTransform);
            Model.Transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private void Start() {
            _controller = GetComponent<Controller>();
            _cameraController.SetTargetYaw(_cameraController.CameraTarget.rotation.eulerAngles.y);
        }

        private void Update() {
            HandleMovement();
        }

        private void LateUpdate() {
            HandleCameraMovement();
        }

        private void HandleMovement() {
            float targetSpeed = _input.IsSprinting ? _characteristics.MaxSprintSpeed : _characteristics.MaxSpeed;
            if (_input.Move == Vector2.zero)
                targetSpeed = 0;
            else
                AlignRotation();

            _controller.Move(transform.forward * (targetSpeed * Time.deltaTime) + new Vector3(0, G, 0));
        }

        private void AlignRotation() {
            Quaternion inputDirection = Quaternion.LookRotation(new Vector3(_input.Move.x, 0, _input.Move.y));
            Quaternion targetRotation = Quaternion.Euler(0, _cameraController.CameraTarget.rotation.eulerAngles.y, 0) *
                                        inputDirection;
            targetRotation = Quaternion.Lerp(transform.rotation, targetRotation,
                Time.deltaTime * _characteristics.RotationSmoothTime);

            transform.rotation = targetRotation;
        }

        private void HandleCameraMovement() => _cameraController.HandleCameraMovement(_input.Look);


#if UNITY_EDITOR
        private void OnValidate() {
            if (_controller == null)
                _controller = GetComponent<Controller>();
        }
#endif
    }
}