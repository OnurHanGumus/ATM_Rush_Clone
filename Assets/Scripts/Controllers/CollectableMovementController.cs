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
    private Rigidbody _rig;
    #endregion
    #endregion

    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        ConnectedNode = transform; //hata vermesin diye bu yapıldı. Aslında kendinden bir öncekinin pozisyonuna gidiyor olmalı.
    }

    void Update()
    {
        if (_isReadyToMove)
        {
            LerpMove();
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
        Vector3 targetPos = ConnectedNode.position;
        targetPos.z += _collectableData.lerpData.lerpSpaces;
        //transform.position = Vector3.Lerp(transform.position, targetPos, _collectableData.lerpData.lerpSoftness * Time.deltaTime);

        _rig.position = new Vector3(Mathf.Lerp(_rig.position.x, targetPos.x, Time.deltaTime * _collectableData.lerpData.lerpSoftnessX), 
            transform.position.y, 
            Mathf.Lerp(_rig.position.z, targetPos.z, Time.deltaTime * _collectableData.lerpData.lerpSoftnessZ));
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
        //_rig.AddForce(new Vector3(0, 0, 500), ForceMode.Impulse);
        //_rig.position = new Vector3(Random.Range(-4, 4), _rig.position.y, _rig.position.z);
        transform.DOMove(targetLocation, 0.5f);
        //transform.DOJump(new Vector3(transform.position.x, 1, transform.position.z), 1f, 0, 0.5f);
    }

    public void MoveToWinZone()
    {
        ConnectedNode = null;
        transform.DOMove(new Vector3(-10, _rig.position.y, _rig.position.z), .5f);
    }
}