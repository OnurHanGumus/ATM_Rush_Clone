using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "CD_Collectable", menuName = "ATMRush/CD_Collectable", order = 0)]
public class CD_Collectable : ScriptableObject
{
    public CollectableData Data;
    //public CollectableState collectableState;
    public CollectableType collectableType;
}
