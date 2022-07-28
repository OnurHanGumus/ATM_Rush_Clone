using Keys;
using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    [SerializeField] private FixedJoystick fixedJoystick;
    #endregion
    #region private vars
    #endregion
    #endregion

    private void Update()
    {
        if (fixedJoystick.Horizontal > 0.1f || fixedJoystick.Horizontal < -0.1f)
        {
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
            {
                XValue = fixedJoystick.Horizontal,
                ClampValues = 3.5f
            });
        }
        if (fixedJoystick.Horizontal == 0f)
        {
            InputSignals.Instance.onInputReleased?.Invoke();
        }
    }
}