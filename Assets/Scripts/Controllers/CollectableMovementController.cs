using Keys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableMovementController : MonoBehaviour
{
    #region self vars
    #region public vars
    public Transform ConnectedNode;
    #endregion

    #region serializefield vars
    #endregion
    #region private vars
    private bool _isReadyToMove = false;
    private CollectableData _collectableData;
    private CollectableManager _collectableManager;
    private Rigidbody _rig;
    private Sequence _sequence;
    #endregion
    #endregion

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        ConnectedNode = transform; 
    }

    void FixedUpdate()
    {
        if (!_isReadyToMove)
        {
            Stop();
        }
    }
    
    public void DeactivateMovement()
    {
        _isReadyToMove = false;
    }
    
    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }
    
    public void MoveToWinZone()
    {
        if (_sequence == null)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(new Vector3(-8, _rig.position.y, _rig.position.z), .4f));
        }
        
        ConnectedNode = null;
        _sequence.Play();
    }

    public void StopMoveToWinZone()
    {
        if(_sequence != null)
            _sequence.Kill();
    }
}