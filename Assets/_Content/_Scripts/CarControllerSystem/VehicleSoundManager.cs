using UnityEngine;

namespace _Scripts.CarControllerSystem {
    public class VehicleSoundManager : MonoBehaviour {
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AnimationCurve _pitchCurve;

        private void Update() {
            _audioSource.pitch = _pitchCurve.Evaluate(Mathf.Min(_vehicle.CurrentSpeed / _vehicle.MaxSpeed, 1));
        }
    }
}