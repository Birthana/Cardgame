using UnityEngine;

public abstract class ActionTargetAnimation : MonoBehaviour
{
    /// When this returns true, the game object this is attached to will automatically be cleaned
    /// up.
    public abstract bool AnimationComplete();

    /// When the animation is created, the target of the corresponding action will be made a child
    /// of whatever game object this function returns. For example, if this animation was created to
    /// show the player attacking an enemy, the corresponding enemy prefab would be made a child of
    /// whatever object this returns so that it can be animated by manipulating that object.
    protected abstract GameObject GetTargetMountPoint();

    private Transform targetOldParent = null;
    private GameObject mountedTarget = null;

    public void MountTarget(GameObject target)
    {
        GameObject mountPoint = GetTargetMountPoint();
        if (mountPoint == null) return;
        targetOldParent = target.transform.parent;
        target.transform.parent = mountPoint.transform;
        mountedTarget = target;
    }

    public void UnmountTarget()
    {
        if (mountedTarget != null)
        {
            mountedTarget.transform.parent = targetOldParent;
            mountedTarget = null;
        }

    }
}
