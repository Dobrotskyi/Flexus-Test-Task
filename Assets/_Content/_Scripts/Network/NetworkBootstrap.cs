using _Scripts.Character.Implementation;
using _Scripts.Character.InteractionSystem;
using _Scripts.Character.StateMachine;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace _Scripts.Network {
    [RequireComponent(typeof(ZenAutoInjecter))]
    public class NetworkBootstrap : NetworkBehaviour {
        [SerializeField] private CharacterStateMachine _characterPrefab;
        private DiContainer _container;

        [Inject]
        public void Init(DiContainer container) {
            Debug.Log("Injected");
            _container = container;
        }

        public override void OnNetworkSpawn() {
            GameObject spawned = NetworkObject
                .InstantiateAndSpawn(_characterPrefab.gameObject, NetworkManager.Singleton).gameObject;
            spawned.transform.position = transform.position;
            if (!IsOwner) {
                Destroy(spawned.GetComponent<CharacterStateMachine>());
                Destroy(spawned.GetComponent<_Scripts.Character.Implementation.CharacterController>());
                Destroy(spawned.GetComponent<PlayerInteractor>());
                Destroy(spawned.GetComponentInChildren<CharacterAnimator>());
                return;
            }

            CharacterStateMachine stateMachine = spawned.GetComponent<CharacterStateMachine>();
            _container.BindInstance(stateMachine);
            _container.InjectGameObject(spawned);
            stateMachine.TransitionToDefault();
        }
    }
}