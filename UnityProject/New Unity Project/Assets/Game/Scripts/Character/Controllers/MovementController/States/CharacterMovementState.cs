using UnityEngine;
using System.Collections;

public class CharacterMovementState : FSMState
{
    #region Variables

    protected Character _character = null;

    #endregion

    #region Constructors

    public CharacterMovementState(Character character)
    {
        _character = character;
    }

    #endregion

    #region Methods

    #endregion
}
