using UnityEngine;

namespace _Scripts.Character.Implementation {
    public class CharacterModelInstaller : MonoBehaviour {
        [SerializeField] private CharacterModel _model;
        [SerializeField] private CharacterController _controller;

        private void Awake() {
            _controller.SetModel(_model);
        }
    }
}