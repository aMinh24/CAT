using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Button interact;
    public InteractItem curItem;
    public Dictionary<Items, InteractItem> canUseItems = new Dictionary<Items, InteractItem>();
    public CinemachineConfiner confiner;
    private bool firstInteract = true;
    private void Awake()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<IngameUI>(this,true);
        }
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
            TutorialManager.Instance.NextTutorial();        //2
            firstInteract = false;
        }
        interact.gameObject.SetActive(true);
        if (curItem != collision.GetComponent<InteractItem>())
        {
            curItem = collision.GetComponent<InteractItem>();
            addItem(curItem);
        }
        
        
    }
    private void addItem(InteractItem item)
    {
        if (item.canUse)
        {
            canUseItems.Add(item.nameItem, item);
        }
    }
    public bool rmItem(Items item)
    {
        if (canUseItems.ContainsKey(item))
        {
            Destroy(canUseItems[item].gameObject);
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
    }
}
