using UnityEngine;
using System.Collections;

public class CharacterMovementBendingState : CharacterMovementState
{
    #region Variables

    #endregion

    #region Constructors

    public CharacterMovementBendingState(Character character):base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnExecute()
    {
        base.OnExecute();

        UpdateBend();
    }

    private void UpdateBend()
    {
        Vector3 currentRotation                       = _character.Capsule.transform.localEulerAngles;
        currentRotation.x                            += _character.Input.IsBendToogle ? Constants.CHARACTER_BENDING_SPEED : -Constants.CHARACTER_BENDING_SPEED;
        _character.Capsule.transform.localEulerAngles = currentRotation;
        float targetXRotation                         = _character.Input.IsBendToogle ? Constants.CHARACTER_BEND_X_ROTATION : Constants.CHARACTER_STAND_X_ROTATION;

       // Debug.Log("currentRotation.x = " + currentRotation.x + " targetXRotation = " + targetXRotation);

        _isCompleted = MathUtils.Approximately(currentRotation.x, targetXRotation, Constants.CHARACTER_BENDING_SPEED);
    }

    protected override void UpdateMovementInput()
    {
        //Do not move when bending
        //base.UpdateMovementInput();
    }

    protected override void UpdateRotationInput()
    {
        //Do not rotate when bending
        //base.UpdateRotationInput();
    }

    #endregion
}
