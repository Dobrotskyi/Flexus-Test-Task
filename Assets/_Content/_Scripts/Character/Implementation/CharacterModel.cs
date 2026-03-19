using _Scripts.Character.Abstracts;
using UnityEngine;
using Controller = UnityEngine.CharacterController;

namespace _Scripts.Character.Implementation {
    public class CharacterModel : MonoBehaviour, ICharacterModel {
        public Transform Transform => transform;
    }
}