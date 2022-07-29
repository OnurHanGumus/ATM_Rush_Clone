using System;
using Enums;
using Keys;
using UnityEngine.Events;
using UnityEngine;

namespace Signals
{
    public class PlayerSignals : MonoBehaviour
    {
        #region self vars
        #region public vars
        public static PlayerSignals Instance;
        #endregion
        #region serializefield vars
        #endregion
        #region private vars

        #endregion
        #endregion

        #region Singleton Awake
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public UnityAction onPlayerAndObstacleCrash = delegate { };
        public UnityAction<Transform> onPlayerAndATMCrash = delegate { };
        public UnityAction onPlayerEnterFinishLine = delegate {  };
    }
}