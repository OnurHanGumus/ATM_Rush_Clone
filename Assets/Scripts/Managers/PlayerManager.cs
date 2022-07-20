using Keys;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class PlayerManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    [SerializeField] PlayerScoreTextController _playerScoreTextController;

    #endregion
    #region private vars
    PlayerMovementController _playerMovementController;
    
    private PlayerData playerData;
    #endregion
    #endregion

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        playerData = GetPlayerData();
        SendPlayerDataToController();
    }
    private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Datas/UnityObjects/CD_Player").Data;

    void Start()
    {
        SubscribeEvents();
    }

    // Update is called once per frame


    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += ActivateMovement;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        PlayerSignals.Instance.onPlayerAndObstacleCrash += OnPlayerAndObstacleCrash;
        ScoreSignals.Instance.onTotalScoreUpdated += UpdateCurrentScore;
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= ActivateMovement;
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        InputSignals.Instance.onInputReleased -= OnInputReleased;
        PlayerSignals.Instance.onPlayerAndObstacleCrash -= OnPlayerAndObstacleCrash;
        ScoreSignals.Instance.onTotalScoreUpdated -= UpdateCurrentScore;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ActivateMovement()
    {
        _playerMovementController.ActivateMovement();
    }

    private void DeactivateMovement()
    {
        _playerMovementController.DeactivateMovement();
    }

    private void SendPlayerDataToController()
    {
        _playerMovementController.SetMovementData(playerData.playerMovementData);
    }

    private void OnInputDragged(HorizontalInputParams horizontalInput)
    {
        _playerMovementController.SetSideForces(horizontalInput);
    }
    private void OnInputReleased()
    {
        _playerMovementController.SetSideForces(0);
    }

    private void OnPlayerAndObstacleCrash()
    {
        _playerMovementController.PushPlayerBack();
    }
    private void UpdateCurrentScore(int score)
    {
        _playerScoreTextController.UpdateScoreText(score);
    }
}
