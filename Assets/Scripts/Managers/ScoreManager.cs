using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region self vars
        #region public vars
        public int atmScore = 0;
        public int playerScore = 0;
        public int totalScore = 0;
        #endregion
        #region serializefield vars

        #endregion
        #region private vars
        #endregion
        #endregion

        #region Event Subscribtion

        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onPlayerScoreUpdated += OnPlayerScoreUpdated;
            ScoreSignals.Instance.onATMScoreUpdated += OnAtmScoreUpdated;
        }
        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onPlayerScoreUpdated -= OnPlayerScoreUpdated;
            ScoreSignals.Instance.onATMScoreUpdated -= OnAtmScoreUpdated;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlayerScoreUpdated(int value)
        {
            playerScore = value;
            UpdateTotalScore(playerScore, atmScore);
            ScoreSignals.Instance.onPlayerScoreUpdated(playerScore);
        }

        private void OnAtmScoreUpdated(int value)
        {
            atmScore += value;
            UpdateTotalScore(playerScore, atmScore);
            ScoreSignals.Instance.onUpdateAtmScore(atmScore);
        }

        private void UpdateTotalScore(int playerScore, int atmScore)
        {
            //Debug.Log("toplam score: " +(playerScore + atmScore));
            ScoreSignals.Instance.onTotalScoreUpdated?.Invoke(playerScore + atmScore);
        }
    }
}