using UnityEngine;

public abstract class BaseCardEffect : MonoBehaviour
{
    public GameObject target;
    public int value;

    public abstract void Resolve();

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
