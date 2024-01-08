
using UnityEngine;

public class TelePoint : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public Transform point;
    public string nameTheme;
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
            if (!string.IsNullOrEmpty(nameTheme))
            {
                AudioManager.Instance.PlayGT(nameTheme);
                DataManager.Instance.dataPlayerSO.curGT = nameTheme;
            }
            if (DataManager.HasInstance)
            {
                DataManager.Instance.SaveGame();
            }
            if (start != null)
            {
                start.SetActive(false);
            }

        }

    }
}
