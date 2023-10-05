using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHide : MonoBehaviour
{
    public bool isHiding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot")) isHiding = true;
        if (collision.CompareTag("Redzone"))
        {
            if (UIManager.HasInstance)
            {
                UIManager.Instance.ShowScreen<FinishLevel>(null,true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot")) isHiding = false;
    }
}
