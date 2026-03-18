using _Scripts.Utils.Data;

namespace _Scripts.Input.Abstracts {
    public interface IVehicleInput {
        public ClampedFloat Throttle { get; }
        public ClampedFloat Brake { get; }
        public ClampedFloat Steering { get; }
    }
}