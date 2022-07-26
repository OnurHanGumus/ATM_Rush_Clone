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

        public void UpgradeMesh(CollectableType type)
        {
            SetActiveFalseAll();
            switch (type)
            {
                case CollectableType.Money:
                    collectables[0].SetActive(true);
                    break;
                case CollectableType.Gold:
                    collectables[1].SetActive(true);
                    break;
                case CollectableType.Gem:
                    collectables[2].SetActive(true);
                    break;
                // default:
                //     collectables[0].SetActive(true);
                //     break;
            }

            // if (collectables[2].gameObject.activeInHierarchy == false)
            // {
            //     SetActiveFalseAll();
            //     collectables[(int)type - 1].SetActive(true);
            // }
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