using UnityEngine;
using System.Collections;

public class CharacterStatsController
{
    #region Variables

    //private Character _character = null;

    private Stat _movementSpeed     = new Stat(Constants.CHARACTER_DEFAULT_SPEED);
    private Stat _health            = new Stat(Constants.CHARACTER_DEFAULT_HEALTH);
    private Stat _score             = new Stat(true);
    private Stat _mana              = new Stat(true);

	private Material _sphereMaterial 					= null; 

    #endregion

    #region Properties

    public Stat MovementSpeed
    {
        get { return _movementSpeed; }
    }

    public Stat Health
    {
        get { return _health; }
    }

    public Stat Score
    {
        get { return _score; }
    }

    public Stat Mana
    {
        get { return _mana; }
    }

    public bool IsDead
    {
        get { return _health.IsEmpty; }
    }

    #endregion

    #region Constructors

    public CharacterStatsController(Character character)
    {
        //_character = character;
		_sphereMaterial = character.PhysicsController.Capsule.GetComponent<MeshRenderer>().sharedMaterial;

		UpdateHealthColor();
	}

    #endregion

    #region Methods

	public void UpdateHealthColor()
	{
		_sphereMaterial.SetColor("_Color", Color.Lerp(new Color(1,0.3f,0.3f), new Color(0.3f,1,0.3f), _health.Percentage));
	}

    public void TakeDamage(float damage, GameObject damageOwner)
    {
		//Debug.Log("damage = " + damage);
        _health.Decrease(damage);
		UpdateHealthColor();
    }

    public void Update()
    {
        _movementSpeed.Update();
        _health.Update();
        _score.Update();
        _mana.Update();
    }

    public void Reset()
    {
        _health.Reset();

		UpdateHealthColor();
    }

    #endregion
}
