using System.Collections.Generic;
using _Scripts.Character.InteractionSystem.Abstracts;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Character.InteractionSystem {
    public class InteractableMb : MonoBehaviour, IInteractable {
        [SerializeField] private UnityEvent _onFocusReceived = new();
        [SerializeField] private UnityEvent _onFocusLost = new();

        private readonly List<IInteraction> _interactions = new();
        [SerializeField] private GameObject _interactionsContainer;

        public bool Enabled { private set; get; } = true;
        public IReadOnlyList<IInteraction> Interactions => _interactions;

        public void OnFocus(bool focus) {
            Debug.Log("On Focus: " + focus);
            if (focus)
                _onFocusReceived.Invoke();
            else
                _onFocusLost.Invoke();
        }

        private void Awake() {
            _interactions.AddRange(_interactionsContainer.GetComponentsInChildren<IInteraction>());
        }
    }
}