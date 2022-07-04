using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        SubscribeEvents();
    }

    // Update is called once per frame


    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnPlay()
    {
        UISignals.Instance.onClosePanel?.Invoke();
    }

    public void PlayBtn()
    {
        CoreGameSignals.Instance?.onPlay();
    }
}
