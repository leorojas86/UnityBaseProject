using UnityEngine;
using System.Collections;

public class CharacterPhysicsController
{
    #region Variables

    private Character _character            = null;
    private Rigidbody _rigidBody            = null;
    private CapsuleCollider _capsule        = null;
    public CollisionNotifier _bottomCollisionNotifier = null;
    public CollisionNotifier _frontCollisionNotifier  = null;

    private bool _isGoingDown   = false;
    private bool _isGoingUp     = false;
    private bool _isLanded      = false;
    private bool _isBended      = false;

    #endregion

    #region Properties

    public Rigidbody RigidBody
    {
        get { return _rigidBody; }
    }

    public CapsuleCollider Capsule
    {
        get { return _capsule; }
    }

    public bool IsFalling
    {
        get { return _isGoingDown; }
    }

    public bool IsJumping
    {
        get { return _isGoingUp; }
    }

    public bool IsLanded
    {
        get { return _isLanded; }
    }

    public bool IsBended
    {
        get { return _isBended; }
        set 
        {
            if(value != _isBended)
            { 
                _isBended = value;

                UpdateBendedState();
            }
        }
    }

    #endregion

    #region Constructors

    public CharacterPhysicsController(Character character)
    {
        _character               = character;
        _rigidBody               = character.GetComponentInChildren<Rigidbody>();
        _capsule                 = character.GetComponentInChildren<CapsuleCollider>();
        _bottomCollisionNotifier = character.transform.Find("Capsule/Collisions/BottomSphere").GetComponent<CollisionNotifier>();
        _frontCollisionNotifier  = character.transform.Find("Capsule/Collisions/FrontSphere").GetComponent<CollisionNotifier>();
        _rigidBody.constraints   = RigidbodyConstraints.FreezeRotation;

        UpdateBendedState();
    }

    #endregion

    #region Methods

    private void UpdateBendedState()
    {
        if(_isBended)
        {
            _bottomCollisionNotifier.OnCollision = null;
            _frontCollisionNotifier.OnCollision  = CheckForIsLanded;
        }
        else
        {
            _bottomCollisionNotifier.OnCollision = CheckForIsLanded;
            _frontCollisionNotifier.OnCollision  = null;
        }

        Vector3 currentRotation             = _capsule.transform.localEulerAngles;
        currentRotation.x                   = _isBended ? Constants.CHARACTER_BEND_X_ROTATION : Constants.CHARACTER_STAND_X_ROTATION;
        _capsule.transform.localEulerAngles = currentRotation;
    }

    public void Update()
    {
        UpdateLandedFlag();

        //Debug.Log("_isLanded " + _isLanded);
    }

    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawCube(_capsule.bounds.center, _capsule.bounds.size);
    }

    private void UpdateLandedFlag()
    {
        if(_isLanded)
            _isLanded = Mathf.Abs(_rigidBody.velocity.y) < Constants.CHARACTER_LANDED_Y_VELOCITY_THRESHOLD;
        else
        {
            _isGoingUp   = _rigidBody.velocity.y > Constants.CHARACTER_LANDED_Y_VELOCITY_THRESHOLD;
            _isGoingDown = _rigidBody.velocity.y < -Constants.CHARACTER_LANDED_Y_VELOCITY_THRESHOLD;

            if(_rigidBody.velocity.y < -Constants.CHARACTER_MAX_FALLING_Y_VELOCITY)
                _character.Die();
        }
    }

    private void CheckForIsLanded(CollisionNotifier.CollisionData data)
    {
        //Debug.Log("data.state = " + data.state);

        switch(data.state)
        {
            case CollisionNotifier.State.Enter:
            case CollisionNotifier.State.Stay:
                if(!_isLanded)
                {
                    _isLanded    = true;
                    _isGoingUp   = false;
                    _isGoingDown = false;
                }
            break;
        }
    }

    public void Reset()
    {
        _isGoingDown    = false;
        _isGoingUp      = false;
        _isLanded       = false;
        IsBended        = false;
    }

    #endregion
}
