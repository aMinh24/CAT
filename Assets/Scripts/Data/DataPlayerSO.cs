using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cat", menuName = "Data/PLayer")]
public class DataPlayerSO: ScriptableObject
{
    [Header("Player")]
    public Vector3 positionCat;
    public List<Items> canUseItems;
    [Header("Environment")]
    public List<Items> collectedItems;
    public List<Items> interactedItems;
    public bool tutorial = true;                    //save first tutorial
    public int curElevator;
}
