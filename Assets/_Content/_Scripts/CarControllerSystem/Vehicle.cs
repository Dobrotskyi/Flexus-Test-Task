using System.Collections.Generic;
using _Scripts.Input.Abstracts;
using _Scripts.Input.Implementation;
using UnityEngine;
using Zenject;

namespace _Scripts.CarControllerSystem {
    public class Vehicle : MonoBehaviour {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<Wheel> _wheels;
        [SerializeField] private float _acceleration = 10;
        [SerializeField] private float _maxSpeed = 100f;
        [SerializeField] private float _breakForce = 20f;
        [SerializeField] private float _maxSteerAngle = 35f;
        [SerializeField] private float _steerSensitivity = 1f;
        [Range(0.1f, 1), SerializeField] private float _steerSnapping = 0.5f;
        [SerializeField] private Transform _centerOfMass;
        private IVehicleInput _input;
        private Rigidbody _rb;
        private bool _onHandbrake;

        public float MaxSpeed => _maxSpeed;
        public float CurrentSpeed { private set; get; }

        [Inject]
        public void Init(IVehicleInput input) {
            _input = input;
        }

        public void SetHandbrakeInput(bool handbrake) {
            _onHandbrake = handbrake;
        }

        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = _centerOfMass.localPosition;
        }

        private void Start() {
            _cameraController.SetTargetYaw(_cameraController.CameraTarget.rotation.eulerAngles.y);
        }

        private void FixedUpdate() {
            CurrentSpeed = _rb.linearVelocity.magnitude;
            WheelsLogic();
        }

        private void LateUpdate() {
            _cameraController.HandleCameraMovement(_input.Look);
        }

        private void WheelsLogic() {
            foreach (var wheel in _wheels) {
                if (wheel.CanBrake)
                    wheel.ApplyBrakeTorque((_onHandbrake ? 1 : _input.Brake.Value) * _breakForce);
                if (wheel.CanPower && !ReachedMaxSpeed())
                    wheel.ApplyMotorTorque(_input.TorqueDirection * _acceleration);
                if (wheel.CanSteer)
                    wheel.ApplySteering(Mathf.Lerp(wheel.SteerAngle,
                        _input.Steering * _maxSteerAngle * _steerSensitivity, _steerSnapping));
            }
        }

        private bool ReachedMaxSpeed() {
            return CurrentSpeed >= _maxSpeed;
        }
    }
}