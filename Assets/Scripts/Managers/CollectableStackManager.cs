using Signals;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    #endregion
    #endregion
    void Awake()
    {
        collectables = new List<Transform>();
        collectables.Add(transform);
        _collectableData = GetCollectableData();

    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CollectableSignals.Instance.getLastNodeOfList += GetLastNodeOfList;

        PlayerSignals.Instance.onPlayerAndObstacleCrash += OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide += OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide += OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide += OnCollectableUpgradeCollide;
        CollectableSignals.Instance.onCollectableATMCollide += OnCollectableAndATMCollide;
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide += OnWalkingPlatformCollide;
    }
    private void UnsubscribeEvents()
    {
        CollectableSignals.Instance.getLastNodeOfList -= GetLastNodeOfList;


        PlayerSignals.Instance.onPlayerAndObstacleCrash -= OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide -= OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide -= OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide -= OnCollectableUpgradeCollide;
        CollectableSignals.Instance.onCollectableATMCollide -= OnCollectableAndATMCollide;
        CollectableSignals.Instance.onCollectableWalkingPlatformCollide -= OnWalkingPlatformCollide;
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
            collectables[i].transform.DOMoveX(collectables[i - 1].transform.position.x, .1f);
            collectables[i].transform.position = new Vector3(collectables[i].transform.position.x, collectables[i].transform.position.y, collectables[i - 1].transform.position.z + _collectableData.lerpData.lerpSpaces);

        }

    }

    private void OnCollectableAndCollectableCollide(Transform addedNode)
    {
        AddCollectableToList(addedNode);
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

    private void OnCollectableAndObstacleCollide(Transform kazaYapanObje)
    {
        int kazaYapanObjeninIndeksi = collectables.IndexOf(kazaYapanObje);

        DropCollectables(kazaYapanObjeninIndeksi);
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());

    }
    private void DropCollectables(int kazaYapanObjeninIndeksi)
    {
        for (int i = collectables.Count - 1; i > 0; i--)
        {
            if (collectables.Count > kazaYapanObjeninIndeksi)
            {
                collectables.RemoveAt(i);
                collectables.TrimExcess();

            }
        }
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
            collectables.RemoveAt(i);
            collectables.TrimExcess();
        }
    }

    public void OnCollectableAndATMCollide(Transform atmyeGirenObje)
    {
        int atmyeGirenObjeninIndeksi = collectables.IndexOf(atmyeGirenObje);
        print(atmyeGirenObjeninIndeksi);

        RemoveCollectablesFromList(atmyeGirenObjeninIndeksi);
    }
    private void OnWalkingPlatformCollide(Transform arg)
    {
        RemoveCollectablesFromList(collectables.IndexOf(arg));
    }

    private void RemoveCollectablesFromList(int kazaYapanObjeninIndeksi)
    {
        if (kazaYapanObjeninIndeksi == -1)
        {
            return;
        }
        int collectablesCount = collectables.Count;
        for (int i = collectablesCount - 1; i > 0; i--)
        {
            if (collectables.Count > kazaYapanObjeninIndeksi)
            {
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