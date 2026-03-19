using _Scripts.CarControllerSystem;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Network {
    public class NetworkVehicleSoundManager : NetworkBehaviour {
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AnimationCurve _pitchCurve;

        private void Update() {
            if (IsOwner && NetworkManager.Singleton != null)
                SetPitchRpc(_pitchCurve.Evaluate(Mathf.Min(_vehicle.CurrentSpeed / _vehicle.MaxSpeed, 1)));
        }

        [Rpc(SendTo.Everyone)]
        private void SetPitchRpc(float pitch) {
            _audioSource.pitch = pitch;
        }
    }
}