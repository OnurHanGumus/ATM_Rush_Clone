using Signals;
using UnityEngine;
using TMPro;
namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Seriliaze Variables
        
        [SerializeField] private TextMeshPro scoreText;
        
        #endregion
        
        #endregion

        #region Subscribe Events

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
            ScoreSignals.Instance.onUpdateAtmScore += OnUpdateAtmScore;
        }
        
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateAtmScore -= OnUpdateAtmScore;
        }
        
        #endregion
        
        private void OnUpdateAtmScore(int atmScore)
        {
            scoreText.text = atmScore.ToString();
        }
    }
}