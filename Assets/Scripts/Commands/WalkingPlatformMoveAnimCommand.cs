using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class WalkingPlatformMoveAnimCommand : MonoBehaviour
    {
        #region Self Variables

        #region Seriliazed Variables

        [SerializeField] private float moveSpeed = .5f;
        [SerializeField] private Renderer renderer;

        #endregion

        #endregion

        private void Update()
        {
            float offset = moveSpeed * Time.time;
            renderer.material.SetTextureOffset("_BaseMap", new Vector2(0,offset));
        }
    }
}