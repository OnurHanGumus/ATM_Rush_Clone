using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class ObstacleAnimController : MonoBehaviour
    {
        [Header("DOTween")]
        [SerializeField] private Ease _ease;
        [SerializeField] private Vector3[] path = new Vector3[2];
        [SerializeField] private float speed;
        [SerializeField] private float delay;
        private Sequence _sequence;

        private void Start()
        {
            MoveAnimation();
        }

        private void MoveAnimation()
        { 
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(path[0], (1/speed), false).SetEase(_ease)
                .SetDelay(delay)).Append(transform.DOMove(path[1], (1/speed), false).SetEase(_ease)
                .SetDelay(delay));
            _sequence.SetLoops(-1, LoopType.Restart);
        }
    }
}