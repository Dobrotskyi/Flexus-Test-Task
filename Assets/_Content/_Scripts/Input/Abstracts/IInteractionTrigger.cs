using R3;

namespace _Scripts.Input.Abstracts {
    public interface IInteractionTrigger {
        public Observable<bool> Triggered { get; }
    }
}