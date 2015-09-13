using UnityEngine;
using System.Collections;

public class CharacterStatsController
{
    #region Variables

    private Character _character = null;

    private int _health = Constants.CHARACTER_DEFAULT_HEALTH;
    private int _score  = 0;

    #endregion

    #region Properties

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public bool IsDead
    {
        get { return _health == 0; }
    }

    #endregion

    #region Constructors

    public CharacterStatsController(Character character)
    {
        _character = character;
    }

    #endregion

    #region Methods

    public void Update()
    {

    }

    public void TakeDamage(int damage, Object damageOwner)
    {
        if (!IsDead)
        {
            _health -= damage;

            if (_health <= 0)
                _health = 0;
        }
    }

    public void Reset()
    {
        _health = Constants.CHARACTER_DEFAULT_HEALTH;
        _score = 0;
    }

    #endregion
}
