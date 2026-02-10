using FSM;
using UnityEngine;

public class SkillState : State
{
    public SkillState(int skillIndex)
    {
        _skillIndex = skillIndex;
    }
    private int _skillIndex;
    private CharacterAnimationController _characterAnimationController;
    private CharacterMovementController _characterController;

    public override void OnEnter()
    {
        base.OnEnter();
        _characterAnimationController = parent.GetComponent<CharacterAnimationController>();
        _characterController = parent.GetComponent<CharacterMovementController>();
        _characterController.IsTakeInput = false;
        _characterAnimationController?.PlaySkill(_skillIndex);
    }

    public override void OnExit()
    {
        base.OnExit();
        _characterController.IsTakeInput = true;
    }

}
