using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHide : MonoBehaviour
{
    public bool isHiding = false;
    private List<GameObject> m_Souls;
    private void Start()
    {
        m_Souls = new List<GameObject>();
        this.Register(EventID.EndTime, restartGame);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pot"))
        {
            SpriteRenderer sp = collision.GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.6f);
            isHiding = true;
        }
        if (collision.CompareTag("Redzone"))
        {
            restartGame();
        }
        if (collision.CompareTag("Soul"))
        {
            m_Souls.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            this.Broadcast(EventID.CollectSoul);
        }
    }
    public void restartGame(object data = null)
    {       
        StartCoroutine(restartGameRoutine());
    }
    IEnumerator restartGameRoutine()
    {
        if (UIManager.HasInstance && !isHiding)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            UIManager.Instance.ShowScreen<StartGame>(false, true);
            yield return new WaitForSeconds(0.5f);
            this.Broadcast(EventID.FullSoul);
            foreach (GameObject go in m_Souls)
            {
                go.SetActive(true);
            }
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
        if (collision.CompareTag("Pot"))
        {
            SpriteRenderer sp = collision.GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);
            isHiding = false;
        }
    }
}
