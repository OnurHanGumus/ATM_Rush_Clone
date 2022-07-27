using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Enums;
using Signals;
using UnityEngine.Events;
using DG.Tweening;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables
    #region public vars
    public CollectableType collectableType;
    public CollectableState collectableState = CollectableState.notCollected;
    public Transform connectedNode;

    #endregion
    #region Serialized Variables

    [SerializeField] private CollectableMeshController collectableMeshController;
    #endregion
    #region private var
    private CollectableMovementController _collectableMovementController;

    #endregion
    #endregion

    private void Awake()
    {
        _collectableMovementController = GetComponent<CollectableMovementController>();
        collectableType = GetCollectableType();
    }
    
    private CollectableType GetCollectableType() => Resources.Load<CD_Collectable>("Datas/UnityObjects/CD_Collectable").collectableType;

    public void OnCollectableAndCollectableCollide(Transform otherNode)
    {

        collectableState = CollectableState.collected;
        CollectableSignals.Instance.onCollectableAndCollectableCollide?.Invoke(transform);

    }

    public void OnCollectableAndATMCollide(Transform atmyeGirenObje)
    {
        CollectableSignals.Instance.onCollectableATMCollide?.Invoke(transform);

        ScoreSignals.Instance.onATMScoreUpdated?.Invoke((int)collectableType);
        StackCollectablesToMiniGame();
    }

    public void OnCollectableAndObstacleCollide(Transform crashedNode)
    {
        CollectableSignals.Instance.onCollectableAndObstacleCollide?.Invoke(transform);

        DestroyCollectable();
    }

    private void DestroyCollectable()
    {
        _collectableMovementController.DeactivateMovement();
        Destroy(gameObject);
    }

    public void OnUpgradeCollectableCollide(Transform upgradedNode)
    {
        if (collectableType != CollectableType.Gem)
        {
            ++collectableType;
            collectableMeshController.UpgradeMesh(collectableType);
            CollectableSignals.Instance.onCollectableUpgradeCollide?.Invoke(transform);
        }
    }

    public void OnCollectableAndWalkingPlatformCollide(Transform _transform)
    {
        connectedNode = null;
        _collectableMovementController.DeactivateMovement();
        _collectableMovementController.MoveToWinZone();
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide?.Invoke(transform);
    }

    public void OnCollectableAndWinZoneCollide(Transform _transform)
    {
        StackCollectablesToMiniGame();
        CollectableSignals.Instance.onCollectableWinZoneCollide?.Invoke(transform);
    }

    public void StackCollectablesToMiniGame()
    {
        collectableType = CollectableType.Money;
        collectableMeshController.UpgradeMesh(collectableType);
        _collectableMovementController.StopMoveToWinZone();

        gameObject.SetActive(false);
        CollectableSignals.Instance.onMiniGameStackCollected(gameObject);
    }
}
