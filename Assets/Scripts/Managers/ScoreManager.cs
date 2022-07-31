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
            ScoreSignals.Instance.onBoxPoint += OnBoxPoint;

            CoreGameSignals.Instance.onNextLevel += OnResetLevel;
        }
        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onPlayerScoreUpdated -= OnPlayerScoreUpdated;
            ScoreSignals.Instance.onATMScoreUpdated -= OnAtmScoreUpdated;
            ScoreSignals.Instance.onBoxPoint -= OnBoxPoint;
            CoreGameSignals.Instance.onNextLevel -= OnResetLevel;

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
        }

        private void OnAtmScoreUpdated(int value)
        {
            atmScore += value;
            playerScore -= value;
            UpdateTotalScore(playerScore, atmScore);
            ScoreSignals.Instance.onUpdateAtmScore(atmScore);
        }

        private void UpdateTotalScore(int playerScore, int atmScore)
        {
            //Debug.Log("toplam score: " +(playerScore + atmScore));
            totalScore = playerScore + atmScore;
            ScoreSignals.Instance.onTotalScoreUpdated?.Invoke(totalScore);
        }
        private void OnBoxPoint(float boxpoint)
        {
            int oldValue = ScoreSignals.Instance.loadSavedMoneyValue();
            ScoreSignals.Instance.onCompleteScore?.Invoke((totalScore) * boxpoint + oldValue);
        }

        private void OnResetLevel()
        {
            atmScore = 0;
            OnPlayerScoreUpdated(0);
        }
    }
}