using System;
using System.Collections;
using _Scripts.Character.InteractionSystem.Abstracts;
using R3;
using UnityEngine;

namespace _Scripts.Character.InteractionSystem {
    [Serializable]
    public class InteractionFinder : IDisposable {
        private const string LAYER_NAME = "Interactable";
        private const int MAX_RAYCAST_FROM_CAMERA = 100;

        [SerializeField] private float _maxDistance = 2f;
        [SerializeField] private float _checkingInterval = 0.1f;
        [SerializeField] private Transform _origin;
        private Camera _camera;
        private LayerMask? _interactionMask;
        private bool _running;

        private readonly ReactiveProperty<IInteractable> _foundInteractable = new();

        public ReadOnlyReactiveProperty<IInteractable> FoundInteractable => _foundInteractable;

        public void ConnectCamera(Camera camera) {
            _camera = camera;
        }

        public void Dispose() {
            _foundInteractable.OnCompleted();
            _foundInteractable.Dispose();
            _foundInteractable.Value = null;
        }

        public void StartSearching(MonoBehaviour coroutineRunner) {
            if (_running)
                return;
            _interactionMask ??= LayerMask.GetMask(LAYER_NAME);
            coroutineRunner.StartCoroutine(CheckInteractable());
        }

        public void Stop() {
            _running = false;
            _foundInteractable.Value = null;
        }

        private IEnumerator CheckInteractable() {
            WaitForSecondsRealtime instruction = new(_checkingInterval);
            _running = true;
            while (_running) {
                TryFindInteractable();
                yield return instruction;
            }
        }

        private void TryFindInteractable() {
            Physics.Raycast(
                _camera.transform.position,
                _camera.transform.forward,
                out RaycastHit cameraFocusHit,
                MAX_RAYCAST_FROM_CAMERA,
                _interactionMask.Value);
#if UNITY_EDITOR
            Debug.DrawRay(_camera.transform.position,
                _camera.transform.forward * MAX_RAYCAST_FROM_CAMERA,
                Color.red);
#endif
            IInteractable foundInteractable = cameraFocusHit.collider?.GetComponent<IInteractable>();
            if (foundInteractable != null &&
                Vector3.Distance(_origin.transform.position, cameraFocusHit.point) > _maxDistance) {
                foundInteractable = null;
            }

            _foundInteractable.Value = foundInteractable;
        }
    }
}