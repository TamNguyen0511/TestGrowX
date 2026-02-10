using UnityEngine;

public class Skill1 : ISkill
{
    private GameObject _enemyGo;
    public void SetEnemy(GameObject enemyGo)
    {
        _enemyGo = enemyGo;
    }
    public void Execute(CharacterSkillController characterSkillController)
    {
        if (_enemyGo == null) return;
        _enemyGo.GetComponent<KnockBackReceiver>().ApplyKnockback(_enemyGo.transform.position - characterSkillController.transform.position);
    }

    public void UpdateLogic(CharacterSkillController characterSkillController)
    {
        characterSkillController.OnEndAnimation();
    }
}
