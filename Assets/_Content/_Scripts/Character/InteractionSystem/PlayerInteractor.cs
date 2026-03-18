using _Scripts.Character.InteractionSystem.Abstracts;
using _Scripts.Character.StateMachine;
using _Scripts.Input.Abstracts;
using R3;
using UnityEngine;
using Zenject;

namespace _Scripts.Character.InteractionSystem {
    public class PlayerInteractor : MonoBehaviour {
        [SerializeField] private InteractionFinder _finder = new();
        private CharacterStateMachine _player;
        private IInteraction _selectedInteraction;
        private bool _ignoreEvent;

        public ReadOnlyReactiveProperty<IInteractable> FoundInteractable => _finder.FoundInteractable;

        [Inject]
        private void Construct(IInteractionTrigger interactionTrigger, CharacterStateMachine player) {
            interactionTrigger.Triggered.Subscribe(ReceiveInput).AddTo(this);
            _player = player;
        }

        public void ReceiveInput(bool startInteraction) {
            if (!gameObject.activeInHierarchy)
                return;
            if (startInteraction)
                StartInteraction();
            else
                StopInteraction();
        }

        public Result StartInteraction() {
            if (_selectedInteraction != null
                || _finder.FoundInteractable.CurrentValue == null
                || !_finder.FoundInteractable.CurrentValue.Interactions[0].Enabled)
                return Result.Success;
            _selectedInteraction = _finder.FoundInteractable.CurrentValue.Interactions[0];
            Result isAvailable = _selectedInteraction.Available;
            if (isAvailable.IsFailure)
                return isAvailable;
            _selectedInteraction.Finished.Take(1).Subscribe(_ => _selectedInteraction = null).AddTo(this);
            _selectedInteraction.Perform(_player);
            return Result.Success;
        }

        public void StopInteraction() {
            if (_selectedInteraction == null)
                return;
            Result result = _selectedInteraction.Stop(_player);
            if (result.IsSuccess)
                _selectedInteraction = null;
        }

        private void Awake() {
            _finder.FoundInteractable.Pairwise().Subscribe(OnFoundInteractable).AddTo(this);
            _finder.ConnectCamera(Camera.main);
        }

        private void OnEnable() {
            _ignoreEvent = false;
            _finder.StartSearching(this);
        }

        private void OnDisable() {
            _finder.Stop();
            _ignoreEvent = true;
        }

        private void OnDestroy() {
            _finder.Dispose();
        }

        private void OnFoundInteractable((IInteractable Previous, IInteractable Current) prevAndNext) {
            if (_ignoreEvent)
                return;
            prevAndNext.Previous?.OnFocus(false);
            prevAndNext.Current?.OnFocus(true);
        }
    }
}