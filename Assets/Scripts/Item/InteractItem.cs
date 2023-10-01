

using UnityEngine;

public abstract class InteractItem : MonoBehaviour
{
    public Items nameItem;
    public bool canUse;
    public abstract void Interact(object? data);
}
