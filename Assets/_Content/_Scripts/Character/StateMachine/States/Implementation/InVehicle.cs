using UnityEngine;
using Unity.Cinemachine;

namespace _Scripts.Character.StateMachine.States.Implementation {
    [System.Serializable]
    public class InVehicle : State, IEnterState, IExitState {
        [SerializeField] private CinemachineVirtualCameraBase _cmCamera;

        private InVehicle() { }

        public void Enter() {
            Player.VehicleInput.Enable();
            _cmCamera.gameObject.SetActive(true);
        }

        public void Exit() {
            Player.VehicleInput.Disable();
            _cmCamera.gameObject.SetActive(false);
        }
    }
}