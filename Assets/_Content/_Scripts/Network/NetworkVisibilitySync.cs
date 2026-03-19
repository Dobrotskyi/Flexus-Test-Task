using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Network {
    public class NetworkVisibilitySync : NetworkBehaviour {
        [SerializeField] private Renderer _renderer;

        private void OnEnable() => SetVisibilityRpc(true);
        private void OnDisable() => SetVisibilityRpc(false);

        [Rpc(SendTo.Everyone)]
        private void SetVisibilityRpc(bool active) => _renderer.enabled = active;
    }
}