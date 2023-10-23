using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : InteractItem
{
    public GameObject door;
    public GameObject openDoor;
    public GameObject bubble;
    public bool first = true;
    public override void Interact(object data)
    {
        if (data is GameObject obj)
        {
            Interact inter = obj.GetComponent<Interact>();
           
            if (inter.rmItem(Items.Key))
            {
                openDoor.SetActive(true);
                door.SetActive(false);
                this.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(Bubble());
                
            }
        }
    }
    private IEnumerator Bubble()
    {
        bubble.SetActive(true);
        yield return new WaitForSeconds(1);
        bubble.SetActive(false);
        if (first)
        {
            TutorialManager.Instance.NextTutorial(); //3
            Debug.Log("dor 3");
            first = false;
        }
    }
}
