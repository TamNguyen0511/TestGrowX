using FSMState.Character;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player; // Enemy of enemy

    [SerializeField]
    private CharacterMovementController _enemyMovementController;

    private void Update()
    {
        if (_player == null) return;

        if (Vector3.Distance(transform.position, _player.transform.position) > 1.5f)
            _enemyMovementController.Move(_player.transform.position - transform.position);
        else
        {
            if (_enemyMovementController.IsTakeInput)
            {
                _enemyMovementController.Move(Vector3.zero);
            }

            _enemyMovementController.SetState(new SkillState(1));

        }
    }
}
