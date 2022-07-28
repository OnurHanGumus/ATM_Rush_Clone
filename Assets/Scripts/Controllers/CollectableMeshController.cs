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
        [SerializeField]
        private List<MeshFilter> meshFilter = new List<MeshFilter>();

        #endregion
        #region private vars
        private MeshFilter _meshFilter;

        #endregion

        #endregion

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }
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