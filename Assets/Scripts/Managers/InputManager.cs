using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    
    #endregion
    #region private vars
    #endregion
    #endregion

    void Start()
    {
        SubscribeEvents();
    }

    // Update is called once per frame
    private void Update()
    {
        //InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
        //{
        //    XValue = _moveVector.x,
        //    ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y)
        //});
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputDragged += OnInputDragged;
    }
    private void UnsubscribeEvents()
    {
        InputSignals.Instance.onInputDragged -= OnInputDragged;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private void OnInputDragged()
    {

    }

    

}
