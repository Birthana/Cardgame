using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimOffset : MonoBehaviour
{
    public Animator animator;
    public string parameterName = "Offset";
    public float minimumValue = 0;
    public float maximumValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat(parameterName, Random.Range(minimumValue, maximumValue));
    }
}
