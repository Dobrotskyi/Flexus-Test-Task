using _Scripts.Utils.Data;

namespace _Scripts.Input.Abstracts {
    public interface IVehicleInput {
        public float TorqueDirection { get; }
        public Float0To1 Brake { get; }
        public float Steering { get; }
    }
}