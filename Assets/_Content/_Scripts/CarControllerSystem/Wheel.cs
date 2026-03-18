using UnityEngine;

namespace _Scripts.CarControllerSystem {
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour {
        [SerializeField] private GameObject _model;
        [SerializeField, HideInInspector] private WheelCollider _collider;
        private Vector3 _worldPosition;
        private Quaternion _worldRotation;

        [field: SerializeField] public bool CanPower { private set; get; }
        [field: SerializeField] public bool CanBrake { private set; get; }
        [field: SerializeField] public bool CanSteer { private set; get; }

        public float SteerAngle => _collider.steerAngle;

        public void ApplyMotorTorque(float torque) {
            _collider.motorTorque = torque;
        }

        public void ApplyBrakeTorque(float brakeValue) {
            _collider.brakeTorque = brakeValue;
        }

        public void ApplySteering(float steerAngle) {
            _collider.steerAngle = steerAngle;
        }

        private void Update() {
            AlignWheels();
        }

        private void AlignWheels() {
            _collider.GetWorldPose(out _worldPosition, out _worldRotation);
            _model.transform.SetPositionAndRotation(_worldPosition, _worldRotation);
        }

        private void OnValidate() {
            if (_collider == null)
                _collider = GetComponent<WheelCollider>();
        }
    }
}