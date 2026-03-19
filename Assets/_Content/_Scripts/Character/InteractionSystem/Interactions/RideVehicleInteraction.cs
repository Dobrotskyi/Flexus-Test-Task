using _Scripts.CarControllerSystem;
using _Scripts.Character.InteractionSystem.Abstracts;
using _Scripts.Character.StateMachine;
using _Scripts.Character.StateMachine.States.Implementation;
using R3;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Character.InteractionSystem.Interactions {
    public class RideVehicleInteraction : NetworkBehaviour, IInteraction, IInteractionInfo {
        private readonly Subject<Unit> _finished = new();
        [SerializeField] private InVehicle _inVehicle;
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private Transform _leavingPosition;

        public Observable<Unit> Finished => _finished;
        public IInteractionInfo Info => this;
        public bool Enabled { get; set; } = true;
        public Result Available { get; } = Result.Success;
        public string Name => "Ride a vehicle";
        public string Description => "Gets in the car";

        public void Perform(CharacterStateMachine player) {
            player.TransitionTo(_inVehicle);
            _vehicle.SetHandbrakeInput(false);
            player.Controller.SetModelActive(false);

            NetworkObject networkObject = player.Controller.Model.GetComponent<NetworkObject>();
            if (networkObject == null)
                return;
            RequestOwnershipServerRpc(networkObject.OwnerClientId);
        }

        public Result Stop(CharacterStateMachine player) {
            player.Controller.SetPosition(_leavingPosition.position);
            player.Controller.SetModelActive(true);
            player.TransitionToDefault();
            _finished.OnNext(Unit.Default);
            _vehicle.SetHandbrakeInput(true);

            return Result.Success;
        }

        [Rpc(SendTo.Server)]
        void RequestOwnershipServerRpc(ulong clientId) {
            NetworkObject.ChangeOwnership(clientId);
        }
    }
}