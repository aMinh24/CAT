using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string nameConversation;
    public TextMeshPro txt;
    public GameObject boxChat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager.Instance.ShowScreen<Conversation>(this, true);
        boxChat.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;   
    }
}
