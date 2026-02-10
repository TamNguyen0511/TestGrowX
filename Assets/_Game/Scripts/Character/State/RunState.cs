using FSM;
using UnityEngine;

namespace FSMState.Character
{
    public class RunState : State
    {
        private CharacterAnimationController _characerController;

        public override void OnEnter()
        {
            base.OnEnter();
            _characerController = parent.GetComponent<CharacterAnimationController>();
            _characerController?.PlayRun();
        }
    }
}