using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerScoreTextController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro scoreTxt;

        #endregion
        #region private vars
        #endregion
        #endregion
 
        public void UpdateScoreText(int score)
        {
            scoreTxt.text = score.ToString();
        }
    }
}