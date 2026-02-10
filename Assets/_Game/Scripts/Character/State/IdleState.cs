using FSM;
using UnityEngine;

namespace FSMState.Character
{
    public class IdleState : State
    {
        private CharacterAnimationController _characerController;

        public override void OnEnter()
        {
            base.OnEnter();
            _characerController = parent.GetComponent<CharacterAnimationController>();
            _characerController?.PlayIdle();
        }
    }
}