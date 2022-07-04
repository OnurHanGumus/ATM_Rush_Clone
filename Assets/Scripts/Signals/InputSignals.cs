using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoBehaviour
{
    #region self vars
    #region public vars
    public static InputSignals Instance;
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

    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction onInputDragged = delegate { };
    public UnityAction onInputReleased = delegate { };

}
