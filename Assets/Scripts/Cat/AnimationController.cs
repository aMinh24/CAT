using Spine.Unity;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public void playAnim(CatStateID id)
    {
        switch (id)
        {
            case CatStateID.Idle:
                {
                    skeletonAnimation.state.SetAnimation(0, "Idle", true);
                    break;
                }
            case CatStateID.Move:
                {
                    skeletonAnimation.state.SetAnimation(0, "Walk", true);
                    break;
                }
        }
    }
}
