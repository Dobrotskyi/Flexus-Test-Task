using UnityEngine;

namespace _Scripts.Utils {
    public class FpsLimiter : MonoBehaviour {
        [SerializeField] private int _fpsLimit = -1;

        private void Awake() {
            Application.targetFrameRate = _fpsLimit;
        }
    }
}