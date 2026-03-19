using Unity.Netcode;
using UnityEngine;

namespace _Scripts.OnUi {
    public class NetworkUI : MonoBehaviour {
        public void StartClient() {
            NetworkManager.Singleton.StartClient();
        }

        public void StartServer() {
            NetworkManager.Singleton.StartServer();
        }

        public void StartHost() {
            NetworkManager.Singleton.StartHost();
        }
    }
}