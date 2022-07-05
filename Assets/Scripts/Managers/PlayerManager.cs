using Keys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    #endregion
    #region private vars
    PlayerMovementController playerMovementController;
    private PlayerData playerData;
    #endregion
    #endregion

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
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
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= ActivateMovement;
        InputSignals.Instance.onInputDragged -= OnInputDragged;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ActivateMovement()
    {
        playerMovementController.ActivateMovement();
    }

    private void DeactivateMovement()
    {
        playerMovementController.DeactivateMovement();

    }

    private void SendPlayerDataToController()
    {
        playerMovementController.SetMovementData(playerData.playerMovementData);
    }

    private void OnInputDragged(HorizontalInputParams horizontalInput)
    {
        playerMovementController.SetSideForces(horizontalInput);
    }
}
