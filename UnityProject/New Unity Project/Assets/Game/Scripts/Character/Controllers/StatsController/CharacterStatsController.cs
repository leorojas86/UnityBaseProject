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
    }

    #endregion

    #region Methods

    public void TakeDamage(int damage, Object damageOwner)
    {
        _health.Decrease(damage);
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
        _health.value = Constants.CHARACTER_DEFAULT_HEALTH;
    }

    #endregion
}
