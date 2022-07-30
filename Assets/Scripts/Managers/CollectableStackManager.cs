using Signals;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CollectableStackManager : MonoBehaviour
{
    #region Self Variables
    #region public vars
    public List<Transform> collectables;


    #endregion
    #region Serialized Variables
    #endregion
    #region private vars
    private CollectableData _collectableData;
    private bool _isAnimating = false;
    private Sequence _sequence;

    #endregion
    #endregion
    void Awake()
    {
        collectables = new List<Transform>();
        collectables.Add(transform);
        _collectableData = GetCollectableData();
        _sequence = DOTween.Sequence();

    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerAndObstacleCrash += OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide += OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide += OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide += OnCollectableUpgradeCollide;
        CollectableSignals.Instance.onCollectableATMCollide += OnCollectableAndATMCollide;
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide += OnWalkingPlatformCollide;
        CollectableSignals.Instance.onCollectableWinZoneCollide += OnCollectableAndWinZoneCollide;
    }
    private void UnsubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerAndObstacleCrash -= OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide -= OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide -= OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide -= OnCollectableUpgradeCollide;
        CollectableSignals.Instance.onCollectableATMCollide -= OnCollectableAndATMCollide;
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide -= OnWalkingPlatformCollide;
        CollectableSignals.Instance.onCollectableWinZoneCollide -= OnCollectableAndWinZoneCollide;
    }
    
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>("Datas/UnityObjects/CD_Collectable").Data;

    private void Update()
    {
        StayInTheLine();
    }
    private void StayInTheLine()
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            collectables[i].transform.DOMoveX(collectables[i - 1].transform.position.x, _collectableData.lerpData.lerpSoftnessX);
            collectables[i].transform.position = new Vector3(collectables[i].transform.position.x, collectables[i].transform.position.y, collectables[i - 1].transform.position.z + _collectableData.lerpData.lerpSpaces);
        }
    }

    private void OnCollectableAndCollectableCollide(Transform addedNode)
    {
        collectables.TrimExcess();
        AddCollectableToList(addedNode);
        StartCoroutine(AddCollectableEffect(collectables.Count));
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }

    public void AddCollectableToList(Transform other)
    {
        if (collectables.Contains(other))
        {
            return;
        }

        collectables.Add(other);
        collectables.TrimExcess();
        other.transform.parent = transform;
        other.tag = "Player";
    }

    private void OnCollectableAndObstacleCollide(Transform node)
    {
        RemoveCollectablesFromList(node,false,"Collectable");
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }
    
    private void OnCollectableUpgradeCollide(Transform upgradedNode)
    {
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }

    private void OnPlayerAndObstacleCrash()
    {
        RemoveAllList();
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }

    private void RemoveAllList()
    {
        for (int i = collectables.Count - 1; i > 0; i--)
        {
            collectables[i].tag = "Collectable";
            AddBreakeForce(collectables[i].transform);
            collectables.RemoveAt(i);
            collectables.TrimExcess();
        }
    }

    public void OnCollectableAndATMCollide(Transform collectable)
    {
        RemoveCollectablesFromList(collectable,false,"Collectable");
        CollectableSignals.Instance.onMiniGameStackCollected?.Invoke(collectable.gameObject);
    }
    public void OnCollectableAndWinZoneCollide(Transform collectable)
    {
        RemoveCollectablesFromList(collectable,false,"Collectable");
        CollectableSignals.Instance.onMiniGameStackCollected?.Invoke(collectable.gameObject);
    }
    
    private void OnWalkingPlatformCollide(Transform arg)
    {
        RemoveCollectablesFromList(arg, true, "Untagged");
    }
    private void RemoveCollectablesFromList(Transform node,bool isWalkingArea, string tagName)
    {
        int indexOfNode = collectables.IndexOf(node);
        int collectablesCount = collectables.Count;

        if (indexOfNode == -1)
        {
            return;
        }
        for (int i = collectablesCount - 1; i > 0; i--)
        {
            if (collectables.Count > indexOfNode)
            {
                if (!isWalkingArea)
                {
                    Transform transform = collectables[i].transform;
                    AddBreakeForce(transform);
                }
                collectables[i].tag = tagName;
                collectables.RemoveAt(i);
                collectables.TrimExcess();
            }
        }
    }

    public Transform GetLastNodeOfList(Transform addedNode)
    {
        AddCollectableToList(addedNode);
        return collectables[collectables.Count - 2];
    }
    private void AddBreakeForce(Transform node)
    {
        node.position = new Vector3(Random.Range(-2f, 2f), node.position.y, node.position.z);
        node.DOJump(node.position, 1f, 1, 0.1f);
    }

    public IEnumerator AddCollectableEffect(int collectableCount)
    {
        _sequence.Kill();
        if (!_isAnimating)
        {
            _isAnimating = true;
            for (int i = 1; i < collectables.Count; i++)
            {
                int tersIndex = collectables.Count - i;
                collectables[tersIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                //int index = (collectables.Count - 1) - i;
                _sequence.Append(collectables[tersIndex].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).SetEase(Ease.Flash));
                _sequence.Append(collectables[tersIndex].transform.DOScale( new Vector3(0.75f, 0.75f, 0.75f), 0.2f).SetDelay(0.2f).SetEase(Ease.Flash));
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.05f * collectables.Count);
            _isAnimating = false;
        }
        collectables.TrimExcess();

    }

    public int CalculateStackValue()
    {
        int _score = 0;
        _score = 0;
        int collectableLength = collectables.Count;
        for (int i = 1; i < collectableLength; i++)
        {
            _score += (int)collectables[i].GetComponent<CollectableManager>().collectableType;
        }
        return _score;
    }
}