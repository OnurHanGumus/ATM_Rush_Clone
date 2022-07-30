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
        private int aradakiMesafe = 2;
        private float startAmount = 1f;
        private float increaseAmount = 0.1f;
        // [SerializeField] private List<Color32> colorList = new List<Color32>();

        // private void Awake()
        // {
        //     for (int i = 0; i < 10; i++)
        //     {
        //         Color32 randomColor = new Color32((byte)Random.Range(0, 255),(byte)Random.Range(0, 255),
        //             (byte)Random.Range(0, 255),(byte)255);
        //         colorList.Add(randomColor);
        //     }
        // }

        private void Start()
        {
            startPosition = firstBox.transform.position;
            
            for (int i = 0; i < amount; i++)
            {
                startPosition = new Vector3(0, startPosition.y + aradakiMesafe, startPosition.z);
                startAmount += increaseAmount;
                GameObject obj = Instantiate(prefab, startPosition, quaternion.identity);
            
                Renderer rend = obj.GetComponent<Renderer>();
                Color32 randomColor = new Color32((byte)Random.Range(0, 255),(byte)Random.Range(0, 255),
                    (byte)Random.Range(0, 255),(byte)255);
                rend.material.color = randomColor;
                
                obj.transform.SetParent(transform);
                obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = 
                    startAmount.ToString("0.0") + " X";
            }
        }
    }
}