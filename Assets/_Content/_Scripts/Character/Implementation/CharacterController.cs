using System;
using _Scripts.Character.Abstracts;
using UnityEngine;
using Zenject;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    [RequireComponent(typeof(Controller))]
    public class CharacterController : MonoBehaviour, ICharacterAnimationParameters {
        private const float G = -9.81f;

        [Serializable]
        public struct Characteristics {
            public float MaxSpeed;
            public float MaxSprintSpeed;
            public float CameraSensitivity;
            public float BottomCameraAngle;
            public float TopCameraAngle;
        }

        [SerializeField] private Characteristics _characteristics;
        [SerializeField] private Controller _controller;
        [SerializeField] private Transform _cameraTarget;
        private ICharacterInput _input;
        private float _cinemachineTargetYaw = 0;
        private float _cinemachineTargetPitch = 0;

        public bool IsSprinting => _input.IsSprinting;
        public float Speed => _controller.velocity.magnitude;

        [Inject]
        public void Init(ICharacterInput input) {
            _input = input;
        }

        private void Start() {
            _cinemachineTargetYaw = _cameraTarget.rotation.eulerAngles.y;
        }

        private void Update() {
            HandleMovement();
            if (Application.isFocused && Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
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

            _controller.Move(transform.forward * targetSpeed * Time.deltaTime + new Vector3(0, G, 0));
        }

        private void AlignRotation() {
            Quaternion inputDirection = Quaternion.LookRotation(new Vector3(_input.Move.x, 0, _input.Move.y));
            Quaternion targetRotation = Quaternion.Euler(0, _cameraTarget.rotation.eulerAngles.y, 0) * inputDirection;

            transform.rotation = targetRotation;
        }

        private void HandleCameraMovement() {
            _cinemachineTargetYaw = ClampAngle(
                _cinemachineTargetYaw + _input.Look.x * _characteristics.CameraSensitivity * Time.deltaTime,
                float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(
                _cinemachineTargetPitch - _input.Look.y * _characteristics.CameraSensitivity * Time.deltaTime,
                _characteristics.BottomCameraAngle, _characteristics.TopCameraAngle);
            _cameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0f);
        }

        private float ClampAngle(float angle, float min, float max) {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (_controller == null)
                _controller = GetComponent<Controller>();
        }
#endif
    }
}