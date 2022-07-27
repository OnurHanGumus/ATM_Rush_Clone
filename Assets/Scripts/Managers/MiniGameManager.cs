using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public

        
        #endregion

        #region Seriliazed Field
        [Header("Referances")]
        [SerializeField] private Transform finishPlayerTransform;
        [SerializeField] private Transform moneyHolder;
        [SerializeField] private Transform firstMoney;
        [Space]
        [Header("Adjustments")]
        [SerializeField] private float stackDistanceAmount = 1.5f;
        [SerializeField][Range(0,1)] private float stackUpTimeMultipler;

        #endregion

        #region Private Field

        private Vector3 _nextMoneyTransform;
        private List<GameObject> _Dollars = new List<GameObject>();
        
        #endregion
        
        #endregion

        private void Awake()
        {
            _nextMoneyTransform = firstMoney.position;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onEndGame += MoveFinishPlayerUp;
            CollectableSignals.Instance.onMiniGameStackCollected += StackUpCollectables;
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onEndGame -= MoveFinishPlayerUp;
            CollectableSignals.Instance.onMiniGameStackCollected -= StackUpCollectables;
        }
        
        #endregion

        public void StackUpCollectables(GameObject collectable)
        {
            _Dollars.Add(collectable);
            collectable.tag = "Untagged";
            collectable.transform.SetParent(moneyHolder);
            _nextMoneyTransform = new Vector3(_nextMoneyTransform.x,_nextMoneyTransform.y - stackDistanceAmount,_nextMoneyTransform.z); 
            collectable.transform.position = _nextMoneyTransform;
            collectable.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        
        private void MoveFinishPlayerUp()
        {
            StartCoroutine("Move");
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(1.5f);
            SetActiveAllCollectables();
            finishPlayerTransform.DOMove(new Vector3(finishPlayerTransform.position.x,
                finishPlayerTransform.position.y - _nextMoneyTransform.y,
                finishPlayerTransform.position.z),Math.Abs(finishPlayerTransform.position.y / stackUpTimeMultipler));
            //print(Math.Abs(finishPlayerTransform.position.y/stackUpTimeMultipler));
        }

        private void SetActiveAllCollectables()
        {
            foreach (var D in _Dollars)
            {
                D.SetActive(true);
            }
        }
    }
}