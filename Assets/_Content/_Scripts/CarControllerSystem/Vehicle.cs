using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CarControllerSystem {
    public class Vehicle : MonoBehaviour {
        [SerializeField] private List<Wheel> _wheels;
        [SerializeField] private float _acceleration = 10;

        private void FixedUpdate() {
            WheelsLogic();
        }

        private void WheelsLogic() {
            // foreach (var wheel in _wheels)
            //     if (wheel.CanPower)
            //         wheel.ApplyMotorTorque(MoveInput() * _acceleration);
        }
    }
}