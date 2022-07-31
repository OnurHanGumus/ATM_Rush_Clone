using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
public class FinishPlayerPhysicController : MonoBehaviour
{



    private string _boxValueText;
    private float _boxValue;

    public void OnTriggerEnter(Collider other)
    {
       // print("1");
        _boxValueText = other.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        _boxValue = float.Parse(_boxValueText.Split(' ')[0]);
      //  print(_boxValue);

        
    }

    public void OnEnable()
    {
        CoreGameSignals.Instance.onGameEnd += OnGameEnd;
    }

    public void OnDisable()
    {
        CoreGameSignals.Instance.onGameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        ScoreSignals.Instance.onBoxPoint?.Invoke(_boxValue);
    }
}
