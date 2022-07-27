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

            if (other.CompareTag("WalkingPlatform"))
            {
                CollectableAndWalkingPlatformCollide();
            }

            if (other.CompareTag("WinZone"))
            {
                CollectableAndWinZoneCollide();
            }
        }
        
        private void CollectableAndCollectableCollide()
        {
            collectableManager.OnCollectableAndCollectableCollide(transform);
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
            collectableManager.OnCollectableAndATMCollide(transform);
        }
        
        private void CollectableAndWalkingPlatformCollide()
        {
            CollectableSignals.Instance.onCollectableWalkingPlatformCollide?.Invoke(transform);
        }
        
        private void CollectableAndWinZoneCollide()
        {
            CollectableSignals.Instance.onCollectableWinZoneCollide?.Invoke(transform);
        }
    }
}