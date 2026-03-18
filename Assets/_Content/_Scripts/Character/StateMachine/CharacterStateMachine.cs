using _Scripts.Attributes;
using _Scripts.Character.StateMachine.States;
using _Scripts.Input.Abstracts;
using UnityEngine;
using Zenject;

namespace _Scripts.Character.StateMachine {
    public class CharacterStateMachine : MonoBehaviour {
        [SerializeReference, SubclassSelector] private State _defaultState;

        [field: SerializeField] public GameObject PlayerGO { private set; get; }
        public ICharacterInputController CharacterInput { private set; get; }
        public IVehicleInputController VehicleInput { private set; get; }
        public IInteractionTriggerController InteractionInput { private set; get; }

        private State _currentState;

        [Inject]
        public void Init(ICharacterInputController characterInput,
            IVehicleInputController vehicleInput,
            IInteractionTriggerController interactionInput) {
            CharacterInput = characterInput;
            VehicleInput = vehicleInput;
            InteractionInput = interactionInput;
        }

        public void TransitionTo(State state) {
            if (_currentState is IExitState exit)
                exit.Exit();
            _currentState = state;
            _currentState.SetData(this);
            if (_currentState is IEnterState enter)
                enter.Enter();
        }

        public void TransitionToDefault() => TransitionTo(_defaultState);

        private void Awake() {
            TransitionTo(_defaultState);
        }
    }
}