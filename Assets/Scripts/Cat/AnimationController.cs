using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public void FlipCat(int n)
    {
        if (n == 0)
        {
            skeletonAnimation.skeleton.ScaleX = 1;
        }
        else skeletonAnimation.skeleton.ScaleX = -1;
    }
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
            case CatStateID.Jump:
                {
                    //skeletonAnimation.state.SetAnimation(0, "Jump_Up", false);
                    PlayAnimationAtStartTime();
                    break;
                }
            case CatStateID.InAir:
                {
                    skeletonAnimation.state.SetAnimation(0, "Jump_Down", false);
                    break;
                }
            case CatStateID.Landing:
                {       
                    //skeletonAnimation.state.SetAnimation(0, "Jump_Down", false);
                    //skeletonAnimation.state.AddAnimation(0, "Idle", true,0);
                    break;
                }
        }
    }
    void PlayAnimationAtStartTime()
    {
        // Lấy đối tượng Skeleton của SkeletonAnimation
        Skeleton skeleton = skeletonAnimation.Skeleton;

        // Lấy đối tượng TrackEntry của animation cần chơi
        TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, "Jump_Up", false);

        // Đặt thời gian bắt đầu của animation
        trackEntry.TrackTime = 0.4f;

        // Cập nhật animation state để áp dụng thay đổi
        skeletonAnimation.AnimationState.Update(0);

        // Đặt thời gian bắt đầu trực tiếp trong skeleton
        skeleton.SetToSetupPose();
        skeleton.UpdateWorldTransform();

        // Gọi hàm cập nhật của skeletonAnimation để áp dụng thay đổi
        skeletonAnimation.LateUpdate();
    }
}
