using System;
using DG.Tweening;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EndGameBoxColorChangeController : MonoBehaviour
    {
        [SerializeField] [Range(.005f,.015f)]private float colorValueIncremental = 0.01f;
        [SerializeField] [Range(0,1)]private float saturation = 1f;
        [SerializeField] [Range(0,1)]private float brightness = 1f;
        private float _colorValue;
        private bool _canSetColor = false;
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            PlayerSignals.Instance.onFinishPlayerCollideWithBox += OnFinishPlayerCollideWithBox;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            PlayerSignals.Instance.onFinishPlayerCollideWithBox -= OnFinishPlayerCollideWithBox;
        }


        #endregion

        private void OnLevelSuccessful()
        {
            _canSetColor = true;
        }
        
        private void OnFinishPlayerCollideWithBox(GameObject box)
        {
            if (_canSetColor)
            {
                _colorValue += colorValueIncremental;

                if (_colorValue >= 0.9f)
                {
                    _colorValue = 0;
                }

                box.GetComponent<Renderer>().material.color = Color.HSVToRGB(_colorValue, saturation, brightness);
                ScaleUp(box);
            }
        }

        private void ScaleUp(GameObject box)
        {
            box.transform.DOScale(new Vector3(5, 2, 3), 1);
            box.transform.DOLocalMoveZ(-.25f, 1, false);
        }
        
    }
}