using Commands;
using Enums;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables
    public LoadGameCommand loadGameCommand;

    #region Private Variables

    private SaveGameCommand _SaveGameCommand;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;

        loadGameCommand = new LoadGameCommand();
        _SaveGameCommand = new SaveGameCommand();


        //if there is no save file created
        if (!ES3.FileExists())
        {
            ES3.Save("Score", 0);
            ES3.Save("Level", 0);
        }
    }
    private void Start()
    {
        //Kaydedilmiþ deðerlerin sýfýrlanmasýnda kullanýlýr.
        //CoreGameSignals.Instance.onSaveGameData(new SaveGameDataParams() { Level = 0, Money = 0 });

    }

    private void OnEnable()
    {
        SubscribeEvents();
    }


    //private void SubscribeEvents()
    //{
    //    //CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
    //    CoreGameSignals.Instance.onSaveGameData += OnSaveGame;
    //}

    //private void UnsubscribeEvents()
    //{
    //    //CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
    //    CoreGameSignals.Instance.onSaveGameData -= OnSaveGame;
    //}

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onSaveGameData += _SaveGameCommand.OnSaveGameData;//kayýt

        ScoreSignals.Instance.loadSavedLevelValue += LoadSavedLevelValue;//yükleme
        ScoreSignals.Instance.loadSavedMoneyValue += LoadSavedMoneyValue;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onSaveGameData -= _SaveGameCommand.OnSaveGameData;

        ScoreSignals.Instance.loadSavedLevelValue -= LoadSavedLevelValue;
        ScoreSignals.Instance.loadSavedMoneyValue -= LoadSavedMoneyValue;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private int LoadSavedLevelValue()
    {
        return loadGameCommand.OnLoadGameData(SaveLoadStates.Level);
    }
    private int LoadSavedMoneyValue()
    {
        return loadGameCommand.OnLoadGameData(SaveLoadStates.Money);
    }

    //private void OnSaveGame(SaveGameDataParams saveDataParams)
    //{
    //    if (saveDataParams.Level != null)
    //    {
    //        ES3.Save("Level", saveDataParams.Level);
    //    }
    //    if (saveDataParams.Money != null) ES3.Save("Money", saveDataParams.Money);
    //    //if (saveDataParams.SFX != null) ES3.Save("SFX", saveDataParams.SFX);
    //    //if (saveDataParams.VFX != null) ES3.Save("VFX", saveDataParams.VFX);
    //    //if (saveDataParams.Haptic != null) ES3.Save("Haptic", saveDataParams.Haptic);
    //}

    
}