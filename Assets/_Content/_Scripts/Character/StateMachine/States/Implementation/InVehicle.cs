using _Scripts.CarControllerSystem;
using UnityEngine;
using Unity.Cinemachine;

namespace _Scripts.Character.StateMachine.States.Implementation {
    [System.Serializable]
    public class InVehicle : State, IEnterState, IExitState {
        [SerializeField] private CinemachineVirtualCameraBase _cmCamera;
        [SerializeField] private Vehicle _vehicle;

        private InVehicle() { }

        public void Enter() {
            Player.VehicleInput.Enable();
            _vehicle.Active = true;
            _cmCamera?.gameObject.SetActive(true);
        }

        public void Exit() {
            Player.VehicleInput.Disable();
            _vehicle.Active = false;
            _cmCamera?.gameObject.SetActive(false);
        }
    }
}