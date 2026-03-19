using _Scripts.Character.Abstracts;
using Unity.Netcode;
using UnityEngine;
using CharacterController = _Scripts.Character.Implementation.CharacterController;

namespace _Scripts.Network {
    public class CharacterNetworkObject : MonoBehaviour {
        [SerializeField] private CharacterController _controller;

        private void Start() {
            NetworkManager.Singleton.OnClientConnectedCallback += SingletonOnOnClientConnectedCallback;
        }

        private void OnDestroy() {
            if (NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.OnClientConnectedCallback -= SingletonOnOnClientConnectedCallback;
        }

        private void SingletonOnOnClientConnectedCallback(ulong id) {
            NetworkObject playerObject = NetworkManager.Singleton.ConnectedClients[id].PlayerObject;
            if (playerObject.IsOwner && playerObject.TryGetComponent(out ICharacterModel model))
                _controller.SetModel(model);
        }
    }
}