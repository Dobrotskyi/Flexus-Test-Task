using System;
using UnityEngine;

namespace _Scripts.Input.Implementation {
    [Serializable]
    public class CameraController {
        [SerializeField] private float _cameraSensitivity;
        [SerializeField] private float _bottomCameraAngle;
        [SerializeField] private float _topCameraAngle;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        [field: SerializeField] public Transform CameraTarget { private set; get; }

        public void SetTargetYaw(float yaw) {
            _cinemachineTargetYaw = yaw;
        }

        public void HandleCameraMovement(Vector2 input) {
            _cinemachineTargetYaw = ClampAngle(
                _cinemachineTargetYaw + input.x * _cameraSensitivity * Time.deltaTime,
                float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(
                _cinemachineTargetPitch - input.y * _cameraSensitivity * Time.deltaTime,
                _bottomCameraAngle, _topCameraAngle);
            CameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0f);
        }

        private float ClampAngle(float angle, float min, float max) {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}