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
            collectableManager.OnCollectableAndObstacleCollide(transform);
        }

        private void CollectableUpgradeCollide()
        {
            collectableManager.OnUpgradeCollectableCollide(transform);
        }

        private void CollectableAndATMCollide()
        {
            collectableManager.OnCollectableAndATMCollide(transform);
        }
        
        private void CollectableAndWalkingPlatformCollide()
        {
            collectableManager.OnCollectableAndWalkingPlatformCollide(transform);
        }
        
        private void CollectableAndWinZoneCollide()
        {
            collectableManager.OnCollectableAndWinZoneCollide(transform);
        }
    }
}