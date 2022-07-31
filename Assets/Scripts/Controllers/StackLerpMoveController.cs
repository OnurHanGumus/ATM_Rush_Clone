using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackLerpMoveController : MonoBehaviour
{
    public void StayInTheLine(List<Transform> collectables, CollectableData _collectableData)
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            collectables[i].transform.DOMoveX(collectables[i - 1].transform.position.x, _collectableData.lerpData.lerpSoftnessX);
            collectables[i].transform.position = new Vector3(collectables[i].transform.position.x, collectables[i].transform.position.y, collectables[i - 1].transform.position.z + _collectableData.lerpData.lerpSpaces);
        }
    }
}
