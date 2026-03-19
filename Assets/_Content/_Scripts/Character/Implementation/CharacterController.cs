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
        private ICharacterInput _input;

        private Controller Controller => Model?.Controller;
        public bool IsSprinting => _input.IsSprinting;
        public float Speed => Controller.velocity.magnitude;
        public CharacterModel Model { private set; get; }

        [Inject]
        public void SetInput(ICharacterInput input) {
            _input = input;
        }

        public void SetModel(CharacterModel model) {
            Model = model;
            Model.Transform.SetPositionAndRotation(transform.position, transform.rotation);
            Model.SetAnimationParameters(this);
        }

        private void Start() {
            _cameraController.SetTargetYaw(_cameraController.CameraTarget.rotation.eulerAngles.y);
        }

        private void Update() {
            if (Model == null || Model.Transform == null)
                return;
            HandleMovement();
        }

        private void LateUpdate() {
            if (Model == null || Model.Transform == null)
                return;
            HandleCameraMovement();
        }

        private void HandleMovement() {
            float targetSpeed = _input.IsSprinting ? _characteristics.MaxSprintSpeed : _characteristics.MaxSpeed;
            if (_input.Move == Vector2.zero)
                targetSpeed = 0;
            else
                AlignRotation();

            Controller.Move(Model.Transform.forward * (targetSpeed * Time.deltaTime) + new Vector3(0, G, 0));
            _cameraController.CameraTarget.position = Model.CameraTarget.position;
        }

        private void AlignRotation() {
            Quaternion inputDirection = Quaternion.LookRotation(new Vector3(_input.Move.x, 0, _input.Move.y));
            Quaternion targetRotation = Quaternion.Euler(0, _cameraController.CameraTarget.rotation.eulerAngles.y, 0) *
                                        inputDirection;
            targetRotation = Quaternion.Lerp(Model.Transform.rotation, targetRotation,
                Time.deltaTime * _characteristics.RotationSmoothTime);

            Model.Transform.rotation = targetRotation;
        }

        private void HandleCameraMovement() => _cameraController.HandleCameraMovement(_input.Look);
    }
}