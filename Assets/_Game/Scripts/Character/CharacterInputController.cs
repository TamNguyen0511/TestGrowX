using FSM;
using FSMState.Character;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovementController _characterController;
    [SerializeField]
    private CharacterSkillController _characterSkillController;
    [SerializeField]
    private VariableJoystick _joystickInput;
    [SerializeField]
    private Button _btnSkill1;
    [SerializeField]
    private Button _btnSkill2;

    private void Start()
    {
        _btnSkill1.onClick.AddListener(() => _characterSkillController.PlaySkill(1));
        _btnSkill2.onClick.AddListener(() => _characterSkillController.PlaySkill(2));
    }
    private void Update()
    {
        if (!_characterController.IsTakeInput) return;
        if (_joystickInput == null) return;
        Vector3 direction = Vector3.forward * _joystickInput.Vertical + Vector3.right * _joystickInput.Horizontal;

        _characterController.Move(direction);
    }
}
