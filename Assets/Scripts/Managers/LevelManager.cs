using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
     

        #region Self Variables

        #region Public Variables

        [Header("Data")] public LevelData Data;

        [HideInInspector] public int LevelID => _levelID;

        #endregion

        #region Serialized Variables

        [Space][SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        //[SerializeField] private ScoreManager _scoreManager;


        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        [ShowInInspector] private int _score;
        

        #endregion

        #endregion

        protected void Awake()
        {
            _levelID = GetActiveLevel();
            Data = GetLevelData();
            OnInitializeLevel();
        }

        private int GetActiveLevel()
        {
            return ScoreSignals.Instance.loadSavedLevelValue();
        }

        private LevelData GetLevelData()
        {
            int newLevelData = _levelID % Resources.Load<CD_Level>("Datas/UnityObjects/CD_Level").Levels.Count;
            return Resources.Load<CD_Level>("Datas/UnityObjects/CD_Level").Levels[newLevelData];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            ScoreSignals.Instance.onCompleteScore += OnComplateScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            ScoreSignals.Instance.onCompleteScore -= OnComplateScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion



        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onSaveGameData?.Invoke(new SaveGameDataParams()
            {
                Level = _levelID,
                Money = _score
                //Money = _scoreManager.totalScore
            });
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onSaveGameData?.Invoke(new SaveGameDataParams()
           {
                Level = _levelID
            });
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnInitializeLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            var newLevelData = _levelID % Levels.Length;
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
            CoreGameSignals.Instance.onCameraInitialized?.Invoke();
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnComplateScore(float score)
        {
            _score = Convert.ToInt32(score);
        }
    }
}