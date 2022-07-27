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
    private MeshRenderer _meshRenderer;
    private CollectableMovementController _collectableMovementController;
    private CollectableData _collectableData;
    private CollectableStackManager _collectableStackManager;

    private bool _isAnimating = false;
    Sequence sequence;

    #endregion
    #endregion

    
    private void Awake()
    {
        _collectableMovementController = GetComponent<CollectableMovementController>();
        SendCollectableDataToController();
        collectableType = GetCollectableType();
        _collectableStackManager = FindObjectOfType<CollectableStackManager>();
    }

    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }
    
    private void SubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerAndObstacleCrash += OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide += OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide += OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide += OnUpgradeCollectableCollide;
        CollectableSignals.Instance.onCollectableATMCollide += OnCollectableAndATMCollide;
        
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide += OnCollectableAndWalkingPlatformCollide;
        CollectableSignals.Instance.onCollectableWinZoneCollide += OnCollectableAndWinZoneCollide;
    }
    private void UnsubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerAndObstacleCrash -= OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide -= OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide -= OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide -= OnUpgradeCollectableCollide;
        CollectableSignals.Instance.onCollectableATMCollide -= OnCollectableAndATMCollide;
        
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide -= OnCollectableAndWalkingPlatformCollide;
        CollectableSignals.Instance.onCollectableWinZoneCollide -= OnCollectableAndWinZoneCollide;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
    
    private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>("Datas/UnityObjects/CD_Collectable").Data;
    private CollectableType GetCollectableType() => Resources.Load<CD_Collectable>("Datas/UnityObjects/CD_Collectable").collectableType;

    private void OnCollectableAndCollectableCollide(Transform otherNode, Transform parent)
    {
        if (otherNode.Equals(transform) && collectableState.Equals(CollectableState.notCollected))
        {
            SetControllerParentNode(parent);
            collectableState = CollectableState.collected;
            GetBigger();
            _collectableMovementController.ActivateMovement();
        }
    }
    private void OnCollectableAndObstacleCollide(Transform crashedNode)
    {
        if (crashedNode.Equals(transform))
        {
            DestroyCollectable();
            ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(_collectableStackManager.CalculateStackValue());
        }
        else if (collectableState.Equals(CollectableState.collected))
        {
            if (crashedNode.localPosition.z <= transform.localPosition.z)
            {

                ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(_collectableStackManager.CalculateStackValue());

                collectableState = CollectableState.notCollected;
                CollectableBreak();
            }
        }
    }

    private void DestroyCollectable()
    {
        _collectableMovementController.DeactivateMovement();
        Destroy(gameObject);
    }

    public void OnUpgradeCollectableCollide(Transform upgradedNode)
    {
        if (upgradedNode.Equals(transform) && collectableType != CollectableType.Gem)
        {
            ++collectableType;
            collectableMeshController.UpgradeMesh(collectableType);
        }
    }


    private void OnPlayerAndObstacleCrash()
    {
        if (collectableState.Equals(CollectableState.collected))
        {
            collectableState = CollectableState.notCollected;
            CollectableBreak();
        }
    }

    private void OnCollectableAndWalkingPlatformCollide(Transform _transform)
    {
        if (transform == _transform)
        {
            connectedNode = null;
            _collectableMovementController.DeactivateMovement();
            _collectableMovementController.MoveToWinZone();
        }
    }
    
    public void OnCollectableAndWinZoneCollide(Transform _transform)
    {
        if (transform == _transform)
            StackCollectablesToMiniGame();
    }

    private void CollectableBreak()
    {
        if (gameObject.CompareTag("Player"))
        {
            transform.tag = "Collectable";
            _collectableMovementController.DeactivateMovement();
            _collectableMovementController.AddDropForce();
        }
    }

    private void SendCollectableDataToController()
    {
        _collectableData = GetCollectableData();
        _collectableMovementController.SetCollectableData(_collectableData);
    }

    private void OnCollectableAndATMCollide(Transform atmyeGirenObje)
    {
        if (atmyeGirenObje.Equals(transform))
        {
            ScoreSignals.Instance.onATMScoreUpdated?.Invoke((int)collectableType);
            StackCollectablesToMiniGame();
        }
        else if (collectableState.Equals(CollectableState.collected))
        {
            if (atmyeGirenObje.position.z <= transform.position.z)
            {
                ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(_collectableStackManager.CalculateStackValue());
                collectableState = CollectableState.notCollected;
                CollectableBreak();
            }
        }
    }


    public void StackCollectablesToMiniGame()
    {
        collectableType = CollectableType.Money;
        collectableMeshController.UpgradeMesh(collectableType);
        _collectableMovementController.StopMoveToWinZone();
        
        gameObject.SetActive(false);
        CollectableSignals.Instance.onMiniGameStackCollected(gameObject);
    }
    

    public void SetControllerParentNode(Transform parentNode)
    {
        connectedNode = parentNode;
        _collectableMovementController.SetConnectedNode(parentNode);
    }

    public void GetBigger()
    {
        //StartCoroutine(GetBiggerCoroutine());
    }

    public IEnumerator GetBiggerCoroutine()
    {
        if (!_isAnimating)
        {
            _isAnimating = true;

            transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 1f);
            yield return new WaitForSeconds(0.05f);

            if (connectedNode != null && !_collectableStackManager.collectables[1].gameObject.Equals(gameObject))
            {
                connectedNode.GetComponent<CollectableManager>().GetBigger();
            }

            yield return new WaitForSeconds(2f);
            _isAnimating = false;
        }
    }
}
