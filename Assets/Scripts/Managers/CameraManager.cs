using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += PreStartedCamera;
            CoreGameSignals.Instance.onNextLevel += PreStartedCamera;
            CoreGameSignals.Instance.onPlay += InGameCamera;
            CoreGameSignals.Instance.onEndGame += EndGameCamera;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= PreStartedCamera;
            CoreGameSignals.Instance.onNextLevel -= PreStartedCamera;
            CoreGameSignals.Instance.onPlay -= InGameCamera;
            CoreGameSignals.Instance.onEndGame -= EndGameCamera;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void PreStartedCamera()
        {
            animator.Play("PreStartCam");
        }

        private void InGameCamera()
        {
            animator.Play("InGameCam");
        }

        private void EndGameCamera()
        {
            StartCoroutine(EndGameCamera(1));
            
        }
        private IEnumerator EndGameCamera(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.Play("EndGameCam");
        }
    }
}