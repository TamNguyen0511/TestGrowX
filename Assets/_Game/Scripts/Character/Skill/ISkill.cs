using UnityEngine;

public interface ISkill
{
    void Execute(CharacterSkillController characterSkillController);
    void UpdateLogic(CharacterSkillController characterSkillController);
}
