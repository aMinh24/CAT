using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOnGround : MonoBehaviour
{
    public Transform shadow;
    public Vector2 init;
    public Vector2 offset;
    private void Start()
    {
        init = shadow.localScale;
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,100, LayerMask.GetMask("Ground"));
        if (hit.distance > 0.1f)
        {
            shadow.localScale = init- new Vector2(hit.distance/20,hit.distance/20);
        }
        shadow.position = hit.point+offset;
    }
}
