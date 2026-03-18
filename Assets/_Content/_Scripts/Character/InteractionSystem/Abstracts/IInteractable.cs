using System.Collections.Generic;

namespace _Scripts.Character.InteractionSystem.Abstracts {
    public interface IInteractable {
        public bool Enabled { get; }
        public IReadOnlyList<IInteraction> Interactions { get; }

        public void OnFocus(bool focus);
    }
}