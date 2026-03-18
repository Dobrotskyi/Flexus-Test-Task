using UnityEngine;

namespace _Scripts.Utils.Data {
    [System.Serializable]
    public struct ClampedFloat {
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        [SerializeField] private float _value;

        public float Value {
            get => _value;
            set => _value = Mathf.Clamp(_min, _max, value);
        }
    }
}