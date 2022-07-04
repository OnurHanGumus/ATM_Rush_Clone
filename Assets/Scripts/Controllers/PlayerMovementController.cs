using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    #endregion
    #region private vars
    Rigidbody _rig;
    bool _isReadyToMove = false;
    private PlayerMovementData _playerMovementData;
    #endregion
    #endregion

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isReadyToMove)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    public void ActivateMovement()
    {
        _isReadyToMove = true;
    }

    public void DeactivateMovement()
    {
        _isReadyToMove = false;
    }

    private void Move()
    {
        _rig.velocity = new Vector3(0, 0, _playerMovementData.ForwardSpeed);
    }

    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }

    public void SetMovementData(PlayerMovementData movementData)
    {
        _playerMovementData = movementData;
    }
}
