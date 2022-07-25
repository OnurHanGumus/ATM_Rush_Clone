using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public

        
        #endregion

        #region Seriliazed Field

        [SerializeField] private GameObject firstMoney;
        [SerializeField] private float stackDistanceAmount = 1.5f;

        #endregion

        #region Private Field

        private Vector3 nextMoneyTransform;

        #endregion
        
        #endregion

        private void Awake()
        {
            nextMoneyTransform = firstMoney.transform.position;
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
            CollectableSignals.Instance.onMiniGameStackCollected += StackUpCollectables;
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onMiniGameStackCollected -= StackUpCollectables;
        }
        
        #endregion

        public void StackUpCollectables(GameObject collectable)
        {
            collectable.tag = "Untagged";
            collectable.transform.SetParent(firstMoney.transform);
            nextMoneyTransform = new Vector3(nextMoneyTransform.x,nextMoneyTransform.y - stackDistanceAmount,nextMoneyTransform.z); 
            collectable.transform.position = nextMoneyTransform;
        }
    }
}