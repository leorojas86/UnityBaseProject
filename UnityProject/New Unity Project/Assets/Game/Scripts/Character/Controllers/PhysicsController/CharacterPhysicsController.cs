using UnityEngine;
using System.Collections;

public class CharacterPhysicsController
{
    #region Variables

    //private Character _character        = null;
    private Rigidbody _rigidBody        = null;
    private CapsuleCollider _capsule    = null;

    private bool _isGoingDown = false;
    private bool _isGoingUp = false;
    private bool _isLanded = false;
    private bool _isBended = false;

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
        set { _isBended = value; }
    }

    #endregion

    #region Constructors

    public CharacterPhysicsController(Character character)
    {
        //_character              = character;
        _rigidBody              = character.GetComponentInChildren<Rigidbody>();
        _capsule                = character.GetComponentInChildren<CapsuleCollider>();
        _rigidBody.constraints  = RigidbodyConstraints.FreezeRotation;
    }

    #endregion

    #region Methods

    public void Update()
    {
        UpdateLandedFlag();
    }

    private void UpdateLandedFlag()
    {
        if (_isLanded)
            _isLanded = Mathf.Abs(_rigidBody.velocity.y) < Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
        else
        {
            _isGoingUp = _rigidBody.velocity.y > Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
            _isGoingDown = _rigidBody.velocity.y < -Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
        }
    }

    public void CheckForTerrainCollision(Collision collision)
    {
        if (!_isLanded)
        {
            _isLanded = collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain");

            if (_isLanded)
            {
                _isGoingUp = false;
                _isGoingDown = false;
            }
        }
    }

    public void Reset()
    {
        _isGoingDown    = false;
        _isGoingUp      = false;
        _isLanded       = false;
        _isBended       = false;
    }

    #endregion

}
