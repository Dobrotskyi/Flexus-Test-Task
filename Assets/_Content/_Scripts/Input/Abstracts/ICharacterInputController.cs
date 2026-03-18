using System;

namespace _Scripts.Input.Abstracts {
    public interface ICharacterInputController : IDisposable {
        public void Enable();
        public void Disable();
    }
}