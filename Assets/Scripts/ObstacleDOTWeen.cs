using UnityEngine;
using DG.Tweening;

public class ObstacleDOTWeen : MonoBehaviour
{
    public void Start()
    {
        transform.DORotate(new Vector3(0, 360f, 0), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart)
            .SetRelative()
             .SetEase(Ease.Linear);
    }
}
