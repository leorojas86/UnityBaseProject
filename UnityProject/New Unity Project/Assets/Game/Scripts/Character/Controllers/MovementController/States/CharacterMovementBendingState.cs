using UnityEngine;
using System.Collections;

public class CharacterMovementBendingState : CharacterMovementIdleState
{
    #region Variables


    #endregion

    #region Constructors

    public CharacterMovementBendingState(Character character):base(character)
    {
    }

    #endregion

    #region Methods

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExecute()
    {
        base.OnExecute();

        UpdateBend();
    }

    private void UpdateBend()
    {
        Vector3 currentRotation                                         = _character.PhysicsController.Capsule.transform.localEulerAngles;
        currentRotation.x                                               += _character.Input.IsBendToogle ? Constants.CHARACTER_BENDING_SPEED : -Constants.CHARACTER_BENDING_SPEED;
        _character.PhysicsController.Capsule.transform.localEulerAngles = currentRotation;
        float targetXRotation                                           = _character.Input.IsBendToogle ? Constants.CHARACTER_BEND_X_ROTATION : Constants.CHARACTER_STAND_X_ROTATION;

       // Debug.Log("currentRotation.x = " + currentRotation.x + " targetXRotation = " + targetXRotation);

        _isCompleted = MathUtils.Approximately(currentRotation.x, targetXRotation, Constants.CHARACTER_BENDING_SPEED);
    }

    public override void OnExit()
    {
        base.OnExit();

        _character.PhysicsController.IsBended = _character.Input.IsBendToogle;

        /*if (_character.IsBended)
           _character.RigidBody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
       else
           _character.RigidBody.constraints = RigidbodyConstraints.FreezeRotation;*/

        _character.Input.ClearLastInput();
    }

    #endregion

    #region Transitions

    public override bool CanBend()
    {
        return false;
    }

    public override bool CanJump()
    {
        return false;
    }

    #endregion
}
