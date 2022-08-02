using Keys;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{
    #region self vars

    #region public vars

    #endregion

    #region serializefield vars

    #endregion

    #region private vars

    Rigidbody _rig;
    private bool _isReadyToMove = false;
    private PlayerMovementData _playerMovementData;
    private float _horizontalInput = 0f;
    private float _clamp = 0f;

    #endregion

    #endregion

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
    }

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
        _rig.velocity = new Vector3(_horizontalInput * _playerMovementData.SidewaysSpeed, _rig.velocity.y,
            _playerMovementData.ForwardSpeed);
    }

    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }

    public void SetMovementData(PlayerMovementData movementData)
    {
        _playerMovementData = movementData;
    }

    public void SetSideForces(HorizontalInputParams horizontalInput)
    {
        _horizontalInput = horizontalInput.XValue;

        _clamp = horizontalInput.ClampValues;

        ClampControl();
    }

    public void SetSideForces(float horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }

    private void ClampControl()
    {
        if ((_horizontalInput < 0 && _rig.position.x <= -_clamp) || (_horizontalInput > 0 && _rig.position.x >= _clamp))
        {
            _horizontalInput = 0;
        }
    }

    public void PushPlayerBack()
    {
        //_rig.AddForce(new Vector3(0, 0, -500), ForceMode.VelocityChange);
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f), 0.75f);
    }
}