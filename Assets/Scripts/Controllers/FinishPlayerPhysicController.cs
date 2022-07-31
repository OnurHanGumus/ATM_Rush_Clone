using UnityEngine;
using TMPro;
using Signals;

namespace Controllers
{
    public class FinishPlayerPhysicController : MonoBehaviour
    {

        #region Self Variables

        #region Private Variables

        private string _boxValueText;
        private float _boxValue;
        #endregion

        #endregion

        #region Event Subscription

        public void OnEnable()
        {
            SubscribeEvents();
        }

        public void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGameEnd += OnGameEnd;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGameEnd -= OnGameEnd;
        }
        #endregion
        
        public void OnTriggerEnter(Collider other)
        {
            _boxValueText = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
            _boxValue = float.Parse(_boxValueText.Split(' ')[0]);
            PlayerSignals.Instance.onFinishPlayerCollideWithBox?.Invoke(other.gameObject);
        }

        private void OnGameEnd()
        {
            ScoreSignals.Instance.onBoxPoint?.Invoke(_boxValue);
        }
    }
}