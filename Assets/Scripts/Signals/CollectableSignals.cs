using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    public class CollectableSignals : MonoBehaviour
    {
        #region self vars
        #region public vars
        public static CollectableSignals Instance;
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
        public Func<Transform, Transform> getLastNodeOfList = delegate { return null; };

        public Func<int> onGetCollectableType = delegate { return 1; };

        public UnityAction<Transform, Transform> onCollectableAndCollectableCollide = delegate { };
        public UnityAction<Transform> onCollectableAndObstacleCollide = delegate { };
        public UnityAction<Transform> onCollectableUpgradeCollide = delegate { };
        public UnityAction<Transform> onCollectableATMCollide = delegate { };
        public UnityAction<Transform> onCollectableWalkingPlatformCollide = delegate {  };
        public UnityAction<Transform> onCollectableWinZoneCollide = delegate {  };
        public UnityAction<GameObject> onMiniGameStackCollected = delegate {  };
    }
}