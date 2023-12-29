using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cat", menuName = "Data/PLayer")]
public class DataPlayerSO: ScriptableObject
{
    [Header("Player")]
    public Vector3 positionCat;
    public string curRoom;
    public List<Items> canUseItems;
    [Header("Environment")]
    public List<Items> collectedItems;
    public List<Items> interactedItems;
    public bool tutorial = true;                    //save first tutorial
    public int curElevator;
    public void Reset()
    {
        curRoom = null;
        positionCat = Vector3.zero;
        canUseItems.Clear();
        collectedItems.Clear();
        interactedItems.Clear();
        tutorial = true;
        curElevator = 0;
    }
}
