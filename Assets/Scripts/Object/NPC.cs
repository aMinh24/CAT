using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string nameConversation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager.Instance.ShowScreen<Conversation>(nameConversation, true);
        //this.enabled = false;
    }
}
