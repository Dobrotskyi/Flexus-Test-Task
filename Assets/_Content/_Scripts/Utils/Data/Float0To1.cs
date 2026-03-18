using UnityEngine;

namespace _Scripts.Utils.Data {
    [System.Serializable]
    public struct Float0To1 {
        private float _value;

        public float Value {
            get => _value;
            set => _value = Mathf.Clamp01(value);
        }
    }
}