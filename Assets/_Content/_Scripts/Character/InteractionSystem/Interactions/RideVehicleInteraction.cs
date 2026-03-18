using _Scripts.CarControllerSystem;
using _Scripts.Character.InteractionSystem.Abstracts;
using _Scripts.Character.StateMachine;
using _Scripts.Character.StateMachine.States.Implementation;
using R3;
using UnityEngine;

namespace _Scripts.Character.InteractionSystem.Interactions {
    public class RideVehicleInteraction : MonoBehaviour, IInteraction, IInteractionInfo {
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
            player.PlayerGO.SetActive(false);
        }

        public Result Stop(CharacterStateMachine player) {
            player.PlayerGO.transform.position = _leavingPosition.position;
            player.PlayerGO.SetActive(true);
            player.TransitionToDefault();
            _finished.OnNext(Unit.Default);
            _vehicle.SetHandbrakeInput(true);
            return Result.Success;
        }
    }
}