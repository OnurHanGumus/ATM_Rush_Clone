using System;
using UnityEngine;

namespace Controllers
{
    public class UiSunShineRotateController : MonoBehaviour
    {
        private bool _canRotate;
        
        private void Update()
        {
            if(_canRotate)
                transform.Rotate(Vector3.forward);
        }

        private void OnEnable()
        {
            _canRotate = true;
        }

        private void OnDisable()
        {
            _canRotate = false;
        }
    }
}