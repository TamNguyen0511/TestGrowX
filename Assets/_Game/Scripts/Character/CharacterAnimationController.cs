using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void PlayIdle()
    {
        if (_animator == null) return;

        _animator.Play("Idle");
    }

    public void PlayRun()
    {
        if (_animator == null) return;

        _animator.Play("Run");
    }
    public void PlaySkill(int skillIndex)
    {
        if (_animator == null) return;

        _animator.Play($"Skill0{skillIndex}");
    }
}
