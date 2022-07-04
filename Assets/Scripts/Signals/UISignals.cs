using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    #region self vars
    #region public vars
    public UISignals Instance;
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
    public UnityAction onOpenPanel = delegate { };
    public UnityAction onUpdateStageData = delegate { };
}
