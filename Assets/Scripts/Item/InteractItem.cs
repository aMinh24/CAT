

using UnityEngine;

public abstract class InteractItem : MonoBehaviour
{
    public Items nameItem;
    public bool canUse;
    private void Awake()
    {
        this.Register(EventID.LoadData, loadGame);
    }
    public void loadGame(object? data)
    {
        if (DataManager.HasInstance)
        {
            if (canUse)
            {
                if (DataManager.Instance.dataPlayerSO.collectedItems.Contains(nameItem))
                {
                    this.gameObject.SetActive(false);
                }
            }
            else if (DataManager.Instance.dataPlayerSO.interactedItems.Contains(nameItem))
            {
                Interact(null, true);
            }
        }
        //Debug.Log("load "+this.gameObject.name);
    }

    public abstract void Interact(object? data, bool forceInteract = false);
}
