using UnityEngine;

public class Skill2 : ISkill
{
    private GameObject _enemyGo;
    private Projectile _projectile;
    private Transform _shootPos;
    public Skill2(GameObject enemyGo, Projectile projectile, Transform shootPos)
    {
        _enemyGo = enemyGo;
        _projectile = projectile;
        _shootPos = shootPos;
    }
    public void Execute(CharacterSkillController characterSkillController)
    {
        _projectile.transform.position = _shootPos.position;
        _projectile.transform.rotation = _shootPos.rotation;

        _projectile.gameObject.SetActive(true);
    }

    public void UpdateLogic(CharacterSkillController characterSkillController)
    {
        characterSkillController.OnEndAnimation();
    }
}
