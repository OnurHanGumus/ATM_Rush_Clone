using Keys;
using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {

    }
    void Start()
    {
        SubscribeEvents();
    }

    // Update is called once per frame
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
       
    }

    private void SubscribeEvents()
    {
        //InputSignals.Instance.onInputDragged += OnInputDragged;
    }
    private void UnsubscribeEvents()
    {
        //InputSignals.Instance.onInputDragged -= OnInputDragged;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    //private void OnInputDragged(HorizontalInputParams horizontalInput)
    //{
    //    PlayerManager.
    //}

    

}
