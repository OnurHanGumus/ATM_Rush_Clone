using UnityEngine;
using Enums;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private AnimationStates animationStates;
        
        #endregion
        #endregion

        private void Awake()
        {
            StartIdleAnim();
        }

        #region EventSubsicription
        private void Start()
        {
            SubscribeEvents();
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += StartRunAnim;
            CoreGameSignals.Instance.onLevelSuccessful += StartFinishAnim;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= StartRunAnim;
            CoreGameSignals.Instance.onLevelSuccessful -= StartFinishAnim;
        }
        #endregion

        #region Animation State Change

        private void ChangeAnimationData(AnimationStates animationStates)
        {
            this.animationStates = animationStates;
        }
        
        private void StartIdleAnim()
        {
            ChangeAnimationData(AnimationStates.Idle);
            ResetAllAnims();
            animator.SetBool("Idle",true);
        }
        
        private void StartRunAnim()
        {
            ChangeAnimationData(AnimationStates.Run);
            ResetAllAnims();
            animator.SetBool("Run",true);
        }
        
        private void StartFinishAnim()
        {
            ChangeAnimationData(AnimationStates.Finish);
        }

        private void ResetAllAnims()
        {
            animator.SetBool("Idle" ,false);
            animator.SetBool("Run" ,false);
        }
        #endregion
    }
}