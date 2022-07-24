using System;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private CollectableManager collectableManager;
        [SerializeField] private GameObject[] collectables = new GameObject[3];

        #endregion
        #region private vars


        #endregion

        #endregion



        #region Event Subsicription
        private void Start()
        {
            SubscribeEvents();
        }
        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            collectableManager.onCollectableTypeChanged += UpgradeMesh;
        }
        
        private void UnSubscribeEvents()
        {
            collectableManager.onCollectableTypeChanged -= UpgradeMesh;
        }

        #endregion

        private void UpgradeMesh(CollectableType type)
        {
            if (collectables[2].gameObject.activeInHierarchy == false)
            {
                SetActiveFalseAll();
                collectables[(int)type - 1].SetActive(true);
            }
        }

        private void SetActiveFalseAll()
        {
            foreach (var collectable in collectables)
            {
                collectable.SetActive(false);
            }
        }
    }
}