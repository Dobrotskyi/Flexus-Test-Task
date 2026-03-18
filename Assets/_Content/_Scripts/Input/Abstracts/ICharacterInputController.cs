using System;

namespace _Scripts.Input.Abstracts {
    public interface ICharacterInputController : IDisposable {
        public ICharacterInput Input { get; }

        public void Init();
        public void Enable();
        public void Disable();
    }
}