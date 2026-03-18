using System;
using Unity.Cinemachine;
using UnityEngine;

namespace _Scripts.Character.StateMachine.States.Implementation {
    [Serializable]
    public class Default : State, IEnterState, IExitState {
        [SerializeField] private CinemachineVirtualCameraBase _cmCamera;

        private Default() { }

        public void Enter() {
            Player.CharacterInput.Enable();
            Player.InteractionInput.Enable();
            _cmCamera.gameObject.SetActive(true);
        }

        public void Exit() {
            Player.CharacterInput.Disable();
            Player.InteractionInput.Disable();
            _cmCamera.gameObject.SetActive(false);
        }
    }
}