using Managers;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion
        #region private vars
        private Animator _animator;
        private AnimationStates _animationStates;
        #endregion

        #endregion

        public void ChangeAnimationData(AnimationStates animationStates)
        {
            throw new System.NotImplementedException();
        }
    }
}