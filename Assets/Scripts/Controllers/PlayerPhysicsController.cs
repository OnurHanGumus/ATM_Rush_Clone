using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
  

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            { 
                PlayerSignals.Instance.onPlayerAndObstacleCrash?.Invoke();
            }
        }
    }
}