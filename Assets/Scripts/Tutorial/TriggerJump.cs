using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TutorialManager.Instance.NextTutorial();        //1
            this.gameObject.SetActive(false);
        }
    }
}
