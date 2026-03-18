using System;
using UnityEngine;

namespace _Scripts.CarControllerSystem.Sounds {
    public class WheelSlidingSoundManager : MonoBehaviour {
        [Serializable]
        public struct WheelAndAudio {
            [field: SerializeField] public Wheel Wheel { private set; get; }
            [field: SerializeField] public AudioSource AudioSource { private set; get; }
        }

        [SerializeField] private WheelAndAudio[] _wheelAudio;
        [SerializeField] private AnimationCurve _volumeCurve;
        [SerializeField] private float _dampTime = 0.1f;
        [SerializeField] private float _fadeoutDampTime = 0.5f;

        private void Update() {
            float targetVolume = 0;
            foreach (var wheelAndAudio in _wheelAudio) {
                if (wheelAndAudio.Wheel.IsSlipping) {
                    targetVolume = _volumeCurve.Evaluate(wheelAndAudio.Wheel.SlippingStrength);
                    targetVolume = Mathf.Lerp(wheelAndAudio.Wheel.SlippingStrength, targetVolume,
                        Time.deltaTime * _dampTime);
                    wheelAndAudio.AudioSource.volume = targetVolume;
                    if (!wheelAndAudio.AudioSource.isPlaying)
                        wheelAndAudio.AudioSource.Play();
                }
                else {
                    if (wheelAndAudio.AudioSource.volume > 0.05f)
                        wheelAndAudio.AudioSource.volume -= _fadeoutDampTime * Time.deltaTime;
                    else if (wheelAndAudio.AudioSource.isPlaying)
                        wheelAndAudio.AudioSource.Stop();
                }
            }
        }
    }
}