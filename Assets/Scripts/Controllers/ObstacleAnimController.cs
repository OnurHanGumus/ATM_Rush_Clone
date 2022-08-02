using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class ObstacleAnimController : MonoBehaviour
    {
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
        [SerializeField] private float delay = 0;

        [Header("Shake")] [Space] 
        [SerializeField] private float shakeSpeed = 1;

        private Sequence _sequence;

        private void Awake()
        {
            _sequence = DOTween.Sequence();
        }

        private void Start()
        {
            
            if(doMove)
                MoveAnimation();
            if(doShake)
                ShakeAnim();
            SetLoop();
        }

        public void MoveAnimation()
        { 
            _sequence.Append(transform.DOMove(path[0], (1/speedStart), false).SetEase(easeStart)
                .SetDelay(delay)).Append(transform.DOMove(path[1], (1/speedEnd), false).SetEase(easeEnd)
                .SetDelay(delay));
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
    }
}