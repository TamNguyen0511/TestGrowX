using FSM;
using FSMState.Character;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private StateMachine _stateMachine;

    [SerializeField]
    private float _moveSpeed = 5;
    private Vector3 _moveDirection;

    public bool IsTakeInput;

    private void Update()
    {
        float inputMagnitude = _moveDirection.magnitude;

        Vector3 moveDirection = new Vector3(_moveDirection.x, 0f, _moveDirection.z).normalized;
        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }
    public State GetCurrentState()
    {
        return _stateMachine.CurrentState;
    }
    public void SetState(State newState)
    {
        if (_stateMachine.CurrentState == newState) return;
        _stateMachine.SetState(newState);
    }

    /// <summary>
    /// Quick use for demo enemy only
    /// </summary>
    public IEnumerator GetPushBack(float time)
    {
        _stateMachine.SetState(new IdleState());
        yield return new WaitForSeconds(time);
        _stateMachine.SetState(new RunState());
        IsTakeInput = true;
    }

    public void Move(Vector3 direction)
    {
        if (!IsTakeInput) return;
        _moveDirection = direction;

        if (_moveDirection.magnitude > 0)
            _stateMachine.SetState(new RunState());
        else _stateMachine.SetState(new IdleState());
    }
}
