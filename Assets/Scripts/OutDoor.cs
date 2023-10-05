using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowScreen<FinishLevel>(null, true);
            }
        }
    }
}
