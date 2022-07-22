using Controllers;
using Enums;
using UnityEngine;
using Signals;

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

    void Start()
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

    private void OnPlayerScoreUpdated(int playerScore)
    {
        this.playerScore = playerScore;
        Debug.Log("oyuncu score: " +playerScore);

        UpdateTotalScore(playerScore, atmScore);
    }

    private void OnAtmScoreUpdated(int deger)
    {
        atmScore += deger;
        Debug.Log("atm score: " +atmScore);
        ScoreSignals.Instance.onUpdateAtmScore(atmScore);
    }

    private void UpdateTotalScore(int playerScore, int atmScore)
    {
        Debug.Log("toplam score: " +(playerScore + atmScore));
        ScoreSignals.Instance.onTotalScoreUpdated?.Invoke(playerScore + atmScore);
    }
}