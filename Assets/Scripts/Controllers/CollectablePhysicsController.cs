using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private CollectableManager collectableManager;

        #endregion
        #region private vars


        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CollectableAndCollectableCollide();
            }

            if (other.CompareTag("Obstacle"))
            {
                CollectableAndObstacleCollide();
            }

            if (other.CompareTag("Upgrade"))
            {
                CollectableUpgradeCollide();
            }
            if (other.CompareTag("ATM"))
            {
                CollectableAndATMCollide();
            }
        }
        private void CollectableAndCollectableCollide()
        {
            Transform parentNode = CollectableSignals.Instance.getLastNodeOfList(transform);
            CollectableSignals.Instance.onCollectableAndCollectableCollide?.Invoke(transform, parentNode);
        }
        private void CollectableAndObstacleCollide()
        {
            CollectableSignals.Instance.onCollectableAndObstacleCollide?.Invoke(transform);
        }

        private void CollectableUpgradeCollide()
        {
            CollectableSignals.Instance.onCollectableUpgradeCollide?.Invoke(transform);
        }

        private void CollectableAndATMCollide()
        {
            CollectableSignals.Instance.onCollectableATMCollide?.Invoke(transform);
        }
    }
}