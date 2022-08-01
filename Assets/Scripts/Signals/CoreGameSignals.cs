using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using Keys;

public class CoreGameSignals : MonoBehaviour
{
    #region self vars
    #region public vars
    public static CoreGameSignals Instance;
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

    public UnityAction onChangeGameState = delegate { };
    public UnityAction onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction onGameEnd = delegate { };
    public UnityAction onCameraInitialized = delegate {  };
    
    public UnityAction<SaveGameDataParams> onSaveGameData = delegate { };

    public Func<int> onGetLevelID = delegate { return 0; };
}