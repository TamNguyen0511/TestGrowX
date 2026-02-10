using FSM;
using FSMState.Character;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSkillController : MonoBehaviour
{
    [SerializeField]
    private StateMachine _stateMachine;
    [SerializeField]
    private GameObject _enemyGo;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private Transform _shootPos;

    /// <summary>
    ///  Quick approach
    /// </summary>

    private Skill1 _skill1 = new();
    private Skill2 _skill2;

    public void PlaySkill(int skillIndex)
    {
        _stateMachine.SetState(new SkillState(skillIndex));
        if (_enemyGo != null)
            transform.parent.LookAt(_enemyGo.transform);
    }
    public void OnEndAnimation()
    {
        _stateMachine.SetState(new IdleState());
    }
    private void PlaySkill1()
    {
        if (_enemyGo == null) return;
        if (_enemyGo.GetComponent<KnockBackReceiver>() == null) return;
        if (Vector3.Distance(_enemyGo.transform.position, transform.position) > 1.5f) return;

        _skill1.SetEnemy(_enemyGo);
        _skill1.Execute(this);
    }
    private void PlaySkill2()
    {
        if (_enemyGo == null) return;
        var newProjectile = Instantiate(_projectile);

        newProjectile.SetEnemy(_enemyGo);

        // Reset target if need
        transform.parent.LookAt(_enemyGo.transform);
        _skill2 = new Skill2(_enemyGo, newProjectile, _shootPos);
        _skill2.Execute(this);
    }
}
