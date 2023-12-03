using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    public string currentState;
    public void ChangeAnimationState(string animationState)
    {
        if(currentState == animationState)
        {
            return;
        }
        animator.Play(animationState);
        currentState = animationState;
    }
    public void PlayAnimation(string state)
    {
        animator.Play(state);
    }
}
