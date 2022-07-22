using System;
using Enums;
using Keys;
using UnityEngine.Events;
using UnityEngine;

namespace Signals
{
    public class ScoreSignals : MonoBehaviour
    {
        #region self vars
        #region public vars
        public static ScoreSignals Instance;
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

        public UnityAction<int> onPlayerScoreUpdated = delegate { };
        public UnityAction<int> onATMScoreUpdated = delegate { };
        public UnityAction<int> onTotalScoreUpdated = delegate { };
        public UnityAction<int> onUpdateAtmScore = delegate {  };
    }
}