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


        #endregion
        #region private vars
        [SerializeField] private TextMeshPro scoreTxt;
        #endregion
        #endregion
 
        public void UpdateScoreText(int score)
        {
            scoreTxt.text = score.ToString();
        }
    }
}