using _Scripts.Input.Abstracts;
using _Scripts.Input.Implementation;
using UnityEngine;
using Zenject;

namespace _Scripts.Input.Installer {
    public class InputSystemInstaller : MonoInstaller {
        [SerializeField] private CharacterInput _input;
        [SerializeField] private VehicleInput _vehicleInput;
        private CharacterInputActions _actions;

        public override void InstallBindings() {
            _actions = new CharacterInputActions();

            _input.Init(_actions.Character);
            _input.Enable();
            _vehicleInput.Init(_actions.Vehicle);
            _vehicleInput.Enable();
            Container.Bind<ICharacterInput>().FromInstance(_input).AsSingle();
            Container.Bind<ICharacterInputController>().FromInstance(_input);
            Container.Bind<IVehicleInput>().FromInstance(_vehicleInput);
            Container.Bind<IVehicleInputController>().FromInstance(_vehicleInput);
        }

        private void OnDestroy() {
            _actions.Disable();
            _actions.Dispose();
            _actions = null;
        }
    }
}