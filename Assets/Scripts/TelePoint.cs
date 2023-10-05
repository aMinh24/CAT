
using UnityEngine;

public class TelePoint : MonoBehaviour
{
    public Transform point;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        collision.gameObject.transform.position = point.position;
    }
}
