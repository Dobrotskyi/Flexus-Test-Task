using System;
using UnityEngine;

namespace _Scripts.Character.Implementation {
    public abstract class AnimatorValueMapping {
        protected readonly Animator Animator;
        protected readonly int StateHash;

        public AnimatorValueMapping(Animator animator, string animatorStateName) {
            Animator = animator;
            StateHash = Animator.StringToHash(animatorStateName);
        }

        public abstract void Check();
    }

    public abstract class AnimatorValueMapping<T> : AnimatorValueMapping where T : IComparable {
        private T _value;
        private Func<T> _getUpdatedValue;


        protected AnimatorValueMapping(Animator animator, string stateName, T startingValue, Func<T> getUpdatedValue) :
            base(animator, stateName) {
            _getUpdatedValue = getUpdatedValue;
            _value = startingValue;
        }

        public override void Check() {
            T current = _getUpdatedValue();
            if (current.CompareTo(_value) != 0)
                UpdateAnimatorValue(current);
            _value = current;
        }

        protected abstract void UpdateAnimatorValue(T newValue);
    }

    public class BoolValueMapping : AnimatorValueMapping<bool> {
        public BoolValueMapping(Animator animator, string stateName, bool startingValue, Func<bool> getUpdatedValue) :
            base(animator, stateName, startingValue, getUpdatedValue) { }

        protected override void UpdateAnimatorValue(bool newValue) {
            Animator.SetBool(StateHash, newValue);
        }
    }

    public class FloatValueMapping : AnimatorValueMapping<float> {
        public FloatValueMapping(Animator animator, string stateName, float startingValue, Func<float> getUpdatedValue)
            : base(animator, stateName, startingValue, getUpdatedValue) { }

        protected override void UpdateAnimatorValue(float newValue) {
            Animator.SetFloat(StateHash, newValue);
        }
    }
}