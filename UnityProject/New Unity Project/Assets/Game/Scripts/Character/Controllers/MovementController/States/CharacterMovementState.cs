using UnityEngine;
using System.Collections;

public class CharacterMovementState : FSMState
{
    #region Variables

    protected Character _character      = null;
    protected bool _lastBendToogleState = false;


    #endregion

    #region Constructors

    public CharacterMovementState(Character character)
    {
        _character = character;
    }

    #endregion

    #region Methods

    public override void OnExecute()
    {
        base.OnExecute();
    }

    #endregion

    #region Transitions

    public bool GoToBendingState()
    {
        return  _character.Input != null && 
                _character.IsLanded && 
                _character.IsBended != _character.Input.IsBendToogle;
    }

    public bool GoToJumpingState()
    {
        return  _character.Input != null && 
                _character.IsLanded && 
                _character.Input.IsJumpTriggered && 
                !_character.Input.IsBendToogle;
    }

    #endregion
}
