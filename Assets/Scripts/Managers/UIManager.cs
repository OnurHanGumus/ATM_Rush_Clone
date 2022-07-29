using Controllers;
using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars

    #endregion
    #region private vars
    UIPanelController uiPanelController;
    #endregion
    #endregion

    private void Awake()
    {
        uiPanelController = GetComponent<UIPanelController>();
    }

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
    }


    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onGameEnd -= OnEndGame;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        UISignals.Instance.onOpenPanel -= OnOpenPanel;
        UISignals.Instance.onClosePanel -= OnClosePanel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    
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
    }
    private void OnRestartLevel()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePanel);
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
        uiPanelController.OpenPanel(panel);
    }
    
    private void OnClosePanel(UIPanels panel)
    {
        uiPanelController.ClosePanel(panel);
    }
}