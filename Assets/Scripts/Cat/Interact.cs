using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public GameObject interact;
    public InteractItem curItem;
    public List<Items> canUseItems = new List<Items> ();
    public CinemachineConfiner confiner;
    public bool firstInteract = true;
    private void Awake()
    {
        this.Register(EventID.LoadData, loadGame);
        this.Register(EventID.saveData, saveGame);
    }
    private void Start()
    {
        this.Broadcast(EventID.StartUI, this);

    }
    public void loadGame(object? data)
    {
        if (DataManager.HasInstance)
        {
            firstInteract = DataManager.Instance.dataPlayerSO.tutorial;
            canUseItems = DataManager.Instance.dataPlayerSO.canUseItems;
            if (!DataManager.Instance.dataPlayerSO.positionCat.Equals(Vector3.zero))
            {
                this.gameObject.transform.position = DataManager.Instance.dataPlayerSO.positionCat;
            }
        }
        //Debug.Log("load interact");
    }
    public void saveGame(object? data)
    {
        DataManager.Instance.dataPlayerSO.positionCat = this.gameObject.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("CameraCollider"))
        {
            confiner.m_BoundingShape2D = collision;
        }

        if (!collision.GetComponent<InteractItem>()) return;
        if (firstInteract)
        {
            this.Broadcast(EventID.Tutorial);
            firstInteract = false;
            DataManager.Instance.dataPlayerSO.tutorial = false;
        }
        interact.gameObject.SetActive(true);
        if (curItem != collision.GetComponent<InteractItem>())
        {
            curItem = collision.GetComponent<InteractItem>();
            
        }
        
        
    }
    private void addItem(InteractItem item)
    {
        if (item.canUse)
        {
            if (canUseItems.Contains(item.nameItem)) { return; }
            if (DataManager.HasInstance)
            {
                DataManager.Instance.dataPlayerSO.canUseItems.Add(item.nameItem);
                DataManager.Instance.dataPlayerSO.collectedItems.Add(item.nameItem);
            }
            canUseItems.Add(item.nameItem);
        }
    }
    public bool rmItem(Items item)
    {
        if (canUseItems.Contains(item))
        {
            DataManager.Instance.dataPlayerSO.canUseItems.Remove(item);
            //Destroy(canUseItems[item].gameObject);
            canUseItems.Remove(item);
            return true;
        }
        return false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interact.gameObject.SetActive(false); 
    }
    public void OnButtonInteract()
    {
        curItem.Interact(this.gameObject);
        addItem(curItem);
        if (DataManager.HasInstance && curItem.nameItem!=Items.None)
        {
            if (!DataManager.Instance.dataPlayerSO.interactedItems.Contains(curItem.nameItem))
            DataManager.Instance.dataPlayerSO.interactedItems.Add(curItem.nameItem);
        }
    }
}
