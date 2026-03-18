using _Scripts.Utils.Data;
using UnityEngine;

namespace _Scripts.Input.Abstracts {
    public interface IVehicleInput {
        public float TorqueDirection { get; }
        public Float0To1 Brake { get; }
        public float Steering { get; }
        public Vector2 Look { get; }
    }
}