using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHide : MonoBehaviour
{
    public bool isHiding = false;
    private bool first;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot"))
        {
            if (!first)
            {
                TutorialManager.Instance.NextTutorial();
                first = true;
            }
            isHiding = true;
        }
        if (collision.CompareTag("Redzone"))
        {
            if (/*UIManager.HasInstance && */!isHiding)
            {
                //UIManager.Instance.ShowScreen<FinishLevel>(null, true);
                GameManager.Instance.LoadScene("Game");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot")) isHiding = false;
    }
}
