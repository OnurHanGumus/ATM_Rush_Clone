using System;
using System.Collections.Generic;
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
        [SerializeField]
        private List<MeshFilter> meshFilter = new List<MeshFilter>();

        #endregion
        #region private vars
        private MeshFilter _meshFilter;

        #endregion

        #endregion

        #region Event Subsicription
        private void OnEnable() 
        { 
            SubscribeEvents();
        }
        private void OnDestroy()
        {
            UnSubscribeEvents();
        }
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.sharedMesh = meshFilter[0].sharedMesh;
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
            switch (type)
            {
                case CollectableType.Money:
                    _meshFilter.sharedMesh = meshFilter[0].sharedMesh;

                    break;
                case CollectableType.Gold:
                    _meshFilter.sharedMesh = meshFilter[1].sharedMesh;

                    break;
                case CollectableType.Gem:
                    _meshFilter.sharedMesh = meshFilter[2].sharedMesh;
                    break;
            }
        }
    }
}