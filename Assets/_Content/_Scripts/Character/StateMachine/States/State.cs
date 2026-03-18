namespace _Scripts.Character.StateMachine.States {
    [System.Serializable]
    public abstract class State {
        protected CharacterStateMachine Player { private set; get; }

        public void SetData(CharacterStateMachine player) {
            Player = player;
        }
    }
}