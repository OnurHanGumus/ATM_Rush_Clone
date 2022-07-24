using System;
using UnityEngine;

namespace Managers
{
    public class FinishAnimationManager : MonoBehaviour
    {
        #region Self Variables

        #region Public

        public Vector3 nextPos;
        
        #endregion

        #region Seriliazed Field
        
        #endregion

        #region Private Field
        

        #endregion
        
        #endregion


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
            
        }

        private void UnSubscribeEvents()
        {
            
        }
        
        #endregion
        
        
    }
}