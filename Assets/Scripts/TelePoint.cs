
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

            if (end != null)
            {
                end.SetActive(true);
                DataManager.Instance.dataPlayerSO.curRoom = end.name;
            }
            collision.gameObject.transform.position = point.position;
            if (DataManager.HasInstance)
            {
                DataManager.Instance.dataPlayerSO.positionCat = point.position;
            }
            if (start != null)
            {
                start.SetActive(false);
            }

        }

    }
}
