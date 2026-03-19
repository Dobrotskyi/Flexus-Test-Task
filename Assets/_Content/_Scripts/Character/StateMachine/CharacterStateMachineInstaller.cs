using UnityEngine;
using Zenject;

namespace _Scripts.Character.StateMachine {
    public class CharacterStateMachineInstaller : MonoInstaller {
        [SerializeField] private CharacterStateMachine _stateMachine;

        public override void InstallBindings() {
            Container.QueueForInject(_stateMachine);
            Container.Bind<CharacterStateMachine>().FromInstance(_stateMachine);
        }

        public override void Start() {
            base.Start();
            _stateMachine.TransitionToDefault();
        }
    }
}