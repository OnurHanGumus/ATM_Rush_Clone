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
        UISignals.Instance.onClosePanel += OnClosePanel;
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        UISignals.Instance.onClosePanel -= OnClosePanel;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    private void OnPlay()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
    }

    public void PlayBtn()
    {
        CoreGameSignals.Instance?.onPlay();
    }

    private void OnClosePanel(UIPanels panel)
    {
        uiPanelController.ClosePanel(panel);
    }
}