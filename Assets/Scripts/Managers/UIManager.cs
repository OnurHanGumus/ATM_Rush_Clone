using System;
using Controllers;
using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Signals;


public class UIManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;

    #endregion
    #region private vars
    UIPanelController _uiPanelController;
    private int _levelID;
    private int _money;

    #endregion
    #endregion

    #region Event Subscription

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onGameEnd += OnEndGame;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        UISignals.Instance.onOpenPanel += OnOpenPanel;
        UISignals.Instance.onClosePanel += OnClosePanel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        ScoreSignals.Instance.onCompleteScore += OnCompleteScore;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onGameEnd -= OnEndGame;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        UISignals.Instance.onOpenPanel -= OnOpenPanel;
        UISignals.Instance.onClosePanel -= OnClosePanel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        ScoreSignals.Instance.onCompleteScore -= OnCompleteScore;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void Awake()
    {
        _uiPanelController = GetComponent<UIPanelController>();
       
    }

    private void Start()
    {
        _levelID = GetLevelIDData();
        _money = GetMoneyData();

        UpdateLevelText(0);
        UpdateMoneyText();
    }

    private int GetLevelIDData()
    {
        return ScoreSignals.Instance.loadSavedLevelValue();
    }

    private int GetMoneyData()
    {
        return ScoreSignals.Instance.loadSavedMoneyValue();

    }

    private void UpdateLevelText(int incremental)
    {
        _levelID += incremental;
        levelText.text = "LEVEL " + (_levelID + 1);
    }

    private void UpdateMoneyText()
    {
        
        moneyText.text = _money.ToString();
    }

   

    private void OnPlay()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
    }
    private void OnEndGame()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.EndGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
    }

    private void OnNextLevel()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePanel);
        UpdateLevelText(1);
    }
    private void OnRestartLevel()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePanel);
    }

    private void OnCompleteScore(float completescore)
    {
        moneyText.text = Convert.ToInt32(completescore).ToString();
    }

    public void PlayBtn()
    {
        CoreGameSignals.Instance.onPlay?.Invoke();
    }

    public void NextLevelBtn()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
    }

    public void RestartBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
    }

    private void OnOpenPanel(UIPanels panel)
    {
        _uiPanelController.OpenPanel(panel);
    }

    private void OnClosePanel(UIPanels panel)
    {
        _uiPanelController.ClosePanel(panel);
    }
}