using R3;
using UnityEngine;

namespace _Scripts.Character.InteractionSystem.Abstracts {
    public interface IInteractionInfo {
        public string Name { get; }
        public string Description { get; }
    }

    public interface IInteraction {
        public Observable<Unit> Finished { get; }

        public IInteractionInfo Info { get; }
        public bool Enabled { get; set; }
        public Result Available { get; }

        public void Perform(GameObject player);
        public Result Stop(GameObject player);
    }
}