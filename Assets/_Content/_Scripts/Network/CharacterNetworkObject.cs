using _Scripts.Character.Implementation;
using Unity.Netcode;
using UnityEngine;
using CharacterController = _Scripts.Character.Implementation.CharacterController;

namespace _Scripts.Network {
    public class CharacterNetworkObject : MonoBehaviour {
        [SerializeField] private CharacterController _controller;

        private void Start() {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        private void OnDestroy() {
            if (NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }

        private void OnClientConnected(ulong id) {
            NetworkObject playerObject = NetworkManager.Singleton.ConnectedClients[id].PlayerObject;
            if (playerObject.IsOwner && playerObject.TryGetComponent(out CharacterModel model))
                _controller.SetModel(model);
        }
    }
}