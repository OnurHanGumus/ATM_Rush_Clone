using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class ObstacleAnimController : MonoBehaviour
    {
    #region Variables
        [Header("Bool")] 
        [SerializeField] private bool doMove = true;
        [SerializeField] private bool doShake = false;
        [Space]
        [Header("Move")][Space]
        [SerializeField] private Ease easeStart = Ease.Linear;
        [SerializeField] private Ease easeEnd = Ease.Linear;
        [Header("Path")][Space]
        [SerializeField] private Vector3[] path = new Vector3[2];
        [Header("Speed")][Space]
        [SerializeField] private float speedStart = 1;
        [SerializeField] private float speedEnd = 1;
        [Header("Delay")] 
        [SerializeField] private float firstDelay = 0;
        [SerializeField] private float secondDelay = 0;

        [Header("Shake")] [Space] 
        [SerializeField] private float shakeSpeed = 1;

        private Sequence _sequence;
    #endregion
    
        #region EventSubscription

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
            
            if(doMove)
                MoveAnimation();
            if(doShake)
                ShakeAnim();
            
            SetLoop();
            
            SetPlay();

            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel += SetPlay;
            CoreGameSignals.Instance.onGameEnd += KillAnim;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel -= SetPlay;
            CoreGameSignals.Instance.onGameEnd -= KillAnim;
        }

        #endregion

        public void MoveAnimation()
        { 
            _sequence.Append(transform.DOLocalMove(path[0], (1/speedStart), false).SetEase(easeStart)
                .SetDelay(firstDelay))
                .Append(transform.DOLocalMove(path[1], (1/speedEnd), false).SetEase(easeEnd)
                .SetDelay(secondDelay));
        }

        private void ShakeAnim()
        {
            _sequence.Append(transform.DOShakePosition(.5f, .5f));
        }
        
        #region Rotate
        private void RotateAnim()
        {
            
        }
        #endregion
        
        private void SetLoop()
        {
            _sequence.SetLoops(-1, LoopType.Restart);
        }

        private void KillAnim()
        {
            _sequence.Kill();
        }
        
        private void SetPlay()
        {
            _sequence.Play();
        }
    }
}