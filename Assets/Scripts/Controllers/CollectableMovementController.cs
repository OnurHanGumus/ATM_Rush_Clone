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
        if (_isReadyToMove)
        {
            //LerpMove();
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

    private void LerpMove()
    {
        if (ConnectedNode != null)
        {
            Vector3 targetPos = ConnectedNode.position;
            targetPos.z += _collectableData.lerpData.lerpSpaces;

            _rig.position = new Vector3(Mathf.Lerp(_rig.position.x, targetPos.x, Time.deltaTime * _collectableData.lerpData.lerpSoftnessX),
                transform.position.y,
                Mathf.Lerp(_rig.position.z, targetPos.z, Time.deltaTime * _collectableData.lerpData.lerpSoftnessZ));
        }
    }

    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }

    public void SetConnectedNode(Transform transform)
    {
        ConnectedNode = transform;
    }

    public void SetCollectableData(CollectableData collectableData)
    {
        _collectableData = collectableData;
    }
    public void AddDropForce()
    {
        Vector3 targetLocation = new Vector3(Random.Range(-4, 4), _rig.position.y, _rig.position.z);
        transform.DOMove(targetLocation, 0.5f);
    }

    public void MoveToWinZone()
    {
        if (_sequence == null)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(new Vector3(-8, _rig.position.y, _rig.position.z), .2f));
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