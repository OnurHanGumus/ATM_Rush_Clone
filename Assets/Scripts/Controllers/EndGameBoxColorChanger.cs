using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EndGameBoxColorChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] boxes;

        private void Awake()
        {
            ChangeColorOfBoxes();
        }

        private void ChangeColorOfBoxes()
        {
            foreach (var box in boxes)
            {
                Renderer rend = box.GetComponent<Renderer>();
                Color32 randomColor = new Color32((byte)Random.Range(0, 255),(byte)Random.Range(0, 255),
                    (byte)Random.Range(0, 255),(byte)255);
                rend.material.color = randomColor;
            }
        }
    }
}