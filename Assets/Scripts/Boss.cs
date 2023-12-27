using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public SkeletonAnimation anim;
    public BossStateID state;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        state = BossStateID.idle;
        
    }

    // Update is called once per frame
    void Update()
    {
        time-=Time.deltaTime;
        if(time<=0)
        {
            time = Random.Range(5f, 10.0f);
            anim.state.SetAnimation(0, "Idle2", false);
            anim.state.AddAnimation(0, "Ilde1", true, 0);
        }

    }
    public void changeState()
    {
        switch (state)
        {
            case BossStateID.idle:
                {
                    anim.state.SetAnimation(0, "Ilde1", true);
                    

                    break;
                }
        }
    }
}
