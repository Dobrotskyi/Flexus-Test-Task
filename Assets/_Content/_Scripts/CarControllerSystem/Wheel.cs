using UnityEngine;

namespace _Scripts.CarControllerSystem {
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour {
        private const float GROUND_SLIP = 0.275f;

        [SerializeField] private Rigidbody _rb;
        [SerializeField] private GameObject _model;

        [SerializeField, HideInInspector] private WheelCollider _collider;
        private Vector3 _worldPosition;
        private Quaternion _worldRotation;
        private WheelHit _wheelHit;
        private float _wheelSlipAmountForward = 0f;
        private float _wheelSlipAmountSideways = 0f;
        private float _totalSlip = 0f;

        [field: SerializeField] public bool CanPower { private set; get; }
        [field: SerializeField] public bool CanBrake { private set; get; }
        [field: SerializeField] public bool CanSteer { private set; get; }

        public float SteerAngle => _collider.steerAngle;
        public bool IsSlipping { private set; get; }
        public float SlippingStrength => _totalSlip - GROUND_SLIP;

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

        private void FixedUpdate() {
            _collider.GetGroundHit(out _wheelHit);
            CheckSlipping();
        }

        private void AlignWheels() {
            _collider.GetWorldPose(out _worldPosition, out _worldRotation);
            _model.transform.SetPositionAndRotation(_worldPosition, _worldRotation);
        }

        private void CheckSlipping() {
            if (_collider.isGrounded) {
                _wheelSlipAmountForward = Mathf.Abs(_wheelHit.forwardSlip);
                _wheelSlipAmountSideways = Mathf.Abs(_wheelHit.sidewaysSlip);
            }
            else {
                _wheelSlipAmountForward = 0f;
                _wheelSlipAmountSideways = 0f;
            }

            _totalSlip = Mathf.Lerp(_totalSlip, ((_wheelSlipAmountSideways + _wheelSlipAmountForward) / 2f),
                Time.fixedDeltaTime * 5f);

            IsSlipping = _totalSlip > GROUND_SLIP && _rb.linearVelocity.magnitude > 1;
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (_collider == null)
                _collider = GetComponent<WheelCollider>();
        }
#endif
    }
}