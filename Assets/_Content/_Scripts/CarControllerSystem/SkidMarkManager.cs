using UnityEngine;

namespace _Scripts.CarControllerSystem {
    public class SkidMarkManager : MonoBehaviour {
        [SerializeField] private Wheel _wheel;
        [SerializeField] private TrailRenderer _trailRenderer;

        private void FixedUpdate() {
            _trailRenderer.emitting = _wheel.IsSlipping;
        }
    }
}