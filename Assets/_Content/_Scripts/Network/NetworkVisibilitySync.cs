using _Scripts.Character.Implementation;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Network {
    public class NetworkVisibilitySync : NetworkBehaviour {
        [SerializeField] private CharacterModel _model;
        private bool _lastValue = false;

        private void Update() {
            if (!IsOwner)
                return;
            if (_lastValue != _model.IsActive) {
                SetActiveRpc(_model.IsActive);
                _lastValue = _model.IsActive;
            }
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void SetActiveRpc(bool visible) {
            _model.SetActive(visible);
        }
    }
}