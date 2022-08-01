using System;
using Signals;
using UnityEngine;
using TMPro;
using DG.Tweening;
namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Seriliaze Variables

        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #endregion

        #region Subscribe Events

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateAtmScore += OnUpdateAtmScore;
            PlayerSignals.Instance.onPlayerAndATMCrash += OnPlayerandATMCollide;
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateAtmScore -= OnUpdateAtmScore;
            PlayerSignals.Instance.onPlayerAndATMCrash -= OnPlayerandATMCollide;
        }

        #endregion

        private void Awake()
        {
            OnUpdateAtmScore(0);
        }

        private void OnUpdateAtmScore(int atmScore)
        {
            scoreText.text = atmScore.ToString();
        }
        
        private void OnPlayerandATMCollide(Transform atm)
        {
            if (atm.Equals(transform))
            {
                atm.DOMoveY(-2.5f, 1);
            }
        }
    }
}