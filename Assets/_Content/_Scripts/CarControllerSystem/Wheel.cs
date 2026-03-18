using UnityEngine;

namespace _Scripts.CarControllerSystem {
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour {
        [SerializeField] private GameObject _model;
        [SerializeField, HideInInspector] private WheelCollider _collider;

        [field: SerializeField] public bool CanPower { private set; get; }

        public void ApplyMotorTorque(float torque) {
            _collider.motorTorque = torque;
        }

        private void OnValidate() {
            if (_collider == null)
                _collider = GetComponent<WheelCollider>();
        }
    }
}