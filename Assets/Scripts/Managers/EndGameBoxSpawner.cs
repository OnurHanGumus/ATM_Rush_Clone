using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EndGameBoxSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject firstBox;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int amount;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private float aradakiMesafe = 3.5f;
        private float startAmount = 1f;
        private float increaseAmount = 0.1f;

        private void Start()
        {
            startPosition = firstBox.transform.position;
            
            for (int i = 0; i < amount; i++)
            {
                startPosition = new Vector3(0, startPosition.y + aradakiMesafe, startPosition.z);
                startAmount += increaseAmount;
                GameObject obj = Instantiate(prefab, startPosition, quaternion.identity);
                obj.transform.SetParent(transform);
                obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = 
                    startAmount.ToString("0.0") + " X";
            }
        }
    }
}