using _Scripts.Character.Abstracts;
using AYellowpaper;
using UnityEngine;

namespace _Scripts.Character.Implementation {
    public class CharacterModelInstaller : MonoBehaviour {
        [SerializeField] private InterfaceReference<ICharacterModel> _model;
        [SerializeField] private CharacterController _controller;

        private void Awake() {
            _controller.SetModel(_model.Value);
        }
    }
}