using _Scripts.Input.Abstracts;
using _Scripts.Utils.Data;
using UnityEngine;

namespace _Scripts.Input.Implementation {
    public class VehicleInput : MonoBehaviour, IVehicleInput, IVehicleInputController {
        private CharacterInputActions.VehicleActions _input;
        private bool _disposed;

        private Float0To1 _throttle;
        private Float0To1 _brake;

        public IVehicleInput Input => this;

        public Float0To1 Brake {
            get {
                _brake.Value = _input.Brake.ReadValue<float>();
                return _brake;
            }
        }

        public float TorqueDirection => _input.TorqueDirection.ReadValue<float>();
        public float Steering => _input.Steering.ReadValue<float>();
        public Vector2 Look => _input.Look.ReadValue<Vector2>();

        public void Init(CharacterInputActions.VehicleActions input) {
            _input = input;
        }

        public void Enable() {
            _input.Enable();
        }

        public void Disable() {
            _input.Disable();
        }
    }
}