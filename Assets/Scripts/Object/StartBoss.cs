using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public FinalRoom room;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        room.bossAnim.state.SetAnimation(0, room.walk, true);
        room.end = false;
        this.enabled = false;
    }

}
