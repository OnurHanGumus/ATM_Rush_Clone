using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class CollectableStackManager : MonoBehaviour
{
    #region Self Variables
    #region public vars
    public List<Transform> collectables;

    #endregion
    #region Serialized Variables
    #endregion
    #region private vars
    private int _score = 0;
    #endregion
    #endregion
    void Awake()
    {
        collectables = new List<Transform>();
        collectables.Add(transform);

    }

    void Start()
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
    }
    private void UnsubscribeEvents()
    {
        CollectableSignals.Instance.getLastNodeOfList -= GetLastNodeOfList;


        PlayerSignals.Instance.onPlayerAndObstacleCrash -= OnPlayerAndObstacleCrash;
        CollectableSignals.Instance.onCollectableAndObstacleCollide -= OnCollectableAndObstacleCollide;
        CollectableSignals.Instance.onCollectableAndCollectableCollide -= OnCollectableAndCollectableCollide;
        CollectableSignals.Instance.onCollectableUpgradeCollide -= OnCollectableUpgradeCollide;
        CollectableSignals.Instance.onCollectableATMCollide -= OnCollectableAndATMCollide;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Update()
    {
        //StayInTheLine();
    }
    //private void StayInTheLine()
    //{
    //    for (int i = 0; i < collectables.Count; i++)
    //    {
    //        if (i == 0)
    //        {
    //            continue;
    //        }
    //        Vector3 targetPos = collectables[i - 1].position;
    //        targetPos.z += lerpSpaces;
    //        collectables[i].position = Vector3.Lerp(collectables[i].position, targetPos, lerpSoftness * Time.deltaTime);
    //    }

    //}

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
            }
        }
    }
    private void OnCollectableAndCollectableCollide(Transform addedNode, Transform parentNode)
    {
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }
    public void AddCollectableToList(Transform other)
    {
        Transform parentNode = collectables[collectables.Count - 1];

        if (collectables.Contains(other))
        {
            return;
        }

        collectables.Add(other);
        other.transform.parent = transform;
        other.tag = "Player";

        CollectableManager collectableManager = other.GetComponent<CollectableManager>();
        collectableManager.SetControllerParentNode(parentNode);
        //collectableMovementController.ActivateMovement();
    }

    private void OnPlayerAndObstacleCrash()
    {
        RemoveAllList();
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }

    private void OnCollectableUpgradeCollide(Transform upgradedNode)
    {
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());
    }

    private void RemoveAllList()
    {
        for (int i = collectables.Count - 1; i > 0; i--)
        {
            collectables.RemoveAt(i);
        }
        collectables.TrimExcess();

    }

    public void OnCollectableAndATMCollide(Transform atmyeGirenObje)
    {
        int atmyeGirenObjeninIndeksi = collectables.IndexOf(atmyeGirenObje);

        DestroyCollectables(atmyeGirenObjeninIndeksi);
        ScoreSignals.Instance.onPlayerScoreUpdated?.Invoke(CalculateStackValue());//Oyuncu üzerindeki collectablenin azaldığının stackmanagere bildirilmesi için var
    }

    private void DestroyCollectables(int kazaYapanObjeninIndeksi)
    {
        for (int i = collectables.Count - 1; i > 0; i--)
        {
            if (collectables.Count > kazaYapanObjeninIndeksi)
            {
                //Destroy(collectables[i].gameObject);
                collectables.RemoveAt(i);
            }
        }
        collectables.TrimExcess();

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
        //CalculateStackValue2();

        return _score;
    }

    public void CalculateStackValue2()
    {
        _score += CollectableSignals.Instance.onGetCollectableType();
        //Debug.Log(_score);
    }
}
