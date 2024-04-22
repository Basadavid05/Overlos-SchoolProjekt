using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        ChangeAnimation("run");
    }


    private void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;

            animator.CrossFade(animation, crossfade);
            
        }
    }
}
