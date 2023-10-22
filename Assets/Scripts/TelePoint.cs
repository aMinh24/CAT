
using UnityEngine;

public class TelePoint : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public Transform point;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (start != null)
            {
                start.SetActive(false);
            }
            collision.gameObject.transform.position = point.position;
            if (end != null)
            {
                end.SetActive(true);
            }  
            
            
        }
        
    }
}
