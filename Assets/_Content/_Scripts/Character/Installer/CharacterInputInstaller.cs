using _Scripts.Character.Abstracts;
using _Scripts.Character.Implementation;
using UnityEngine;
using Zenject;

namespace _Scripts.Character.Installer {
    public class CharacterInputInstaller : MonoInstaller {
        [SerializeField] private CharacterInput _input;

        public override void InstallBindings() {
            _input.Init();
            _input.Enable();
            Container.Bind<ICharacterInput>().FromInstance(_input).AsSingle();
            Container.Bind<ICharacterInputController>().FromInstance(_input);
        }
    }
}