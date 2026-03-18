using _Scripts.Input.Abstracts;
using _Scripts.Input.Implementation;
using UnityEngine;
using Zenject;

namespace _Scripts.Input.Installer {
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(VehicleInput))]
    [RequireComponent(typeof(InteractionTriggeredInput))]
    public class InputSystemInstaller : MonoInstaller {
        [SerializeField] private CharacterInput _input;
        [SerializeField] private VehicleInput _vehicleInput;
        [SerializeField] private InteractionTriggeredInput _interactionInput;
        private CharacterInputActions _actions;

        public override void InstallBindings() {
            _actions = new CharacterInputActions();

            _input.Init(_actions.Character);
            _input.Enable();
            _vehicleInput.Init(_actions.Vehicle);
            _vehicleInput.Enable();
            _interactionInput.Init(_actions.Interaction);
            _interactionInput.Enable();
            Container.Bind<ICharacterInput>().FromInstance(_input).AsSingle();
            Container.Bind<ICharacterInputController>().FromInstance(_input);
            Container.Bind<IVehicleInput>().FromInstance(_vehicleInput);
            Container.Bind<IVehicleInputController>().FromInstance(_vehicleInput);
            Container.Bind<IInteractionTrigger>().FromInstance(_interactionInput);
            Container.Bind<IInteractionTriggerController>().FromInstance(_interactionInput);
        }

        private void OnDestroy() {
            _actions.Disable();
            _actions.Dispose();
            _actions = null;
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (_input == null)
                _input = GetComponent<CharacterInput>();
            if (_vehicleInput == null)
                _vehicleInput = GetComponent<VehicleInput>();
            if (_interactionInput == null)
                _interactionInput = GetComponent<InteractionTriggeredInput>();
        }
#endif
    }
}