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

    public virtual bool CanBend()
    {
        return true;
    }

    public virtual bool CanJump()
    {
        return true;
    }

    #endregion

    #region Transitions

    public virtual bool GoToBendingState()
    {
        return  _character.Input != null && 
                _character.IsLanded && 
                _character.IsBended != _character.Input.IsBendToogle;
    }

    public virtual bool GoToJumpingState()
    {
        return  _character.Input != null && 
                _character.IsLanded && 
                _character.Input.IsJumpTriggered && 
                !_character.Input.IsBendToogle;
    }

    public virtual bool GoToMovingState()
    {
        return  _character.Input != null && 
                (_character.Input.TargetMovement != Vector3.zero || _character.Input.TargetRotation != Vector2.zero);
    }

    public virtual bool GoToIdle()
    {
        return _character.Input != null && _character.IsLanded && _character.Input.TargetMovement == Vector3.zero;
    }

    #endregion
}
