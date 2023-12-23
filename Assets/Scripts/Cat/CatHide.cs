using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHide : MonoBehaviour
{
    public bool isHiding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot"))
        {
            isHiding = true;
        }
        if (collision.CompareTag("Redzone"))
        {
            StartCoroutine(restartGame());
        }
        if (collision.CompareTag("DoorOut"))
        {
            if (UIManager.HasInstance)
            {
                Debug.Log("out");
                UIManager.Instance.ShowScreen<FinishLevel>(null, true);
            }
        }
    }
    IEnumerator restartGame()
    {
        if (UIManager.HasInstance && !isHiding)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            UIManager.Instance.ShowScreen<StartGame>(false, true);
            yield return new WaitForSeconds(0.5f);
            transform.position = DataManager.Instance.dataPlayerSO.positionCat;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Redzone"))
    //    {
    //        if (UIManager.HasInstance && !isHiding)
    //        {

    //            UIManager.Instance.ShowScreen<DeathScreen>(null, true);
    //        }
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot")) isHiding = false;
    }
}
