using UnityEngine;
using DG.Tweening;

public class AxeDOTWeen : MonoBehaviour
{
    public void Start()
    {
        transform.DORotate(new Vector3(0, 0, 120f), 2.5f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo)
            .SetRelative()
             .SetEase(Ease.Linear);
    }
}
