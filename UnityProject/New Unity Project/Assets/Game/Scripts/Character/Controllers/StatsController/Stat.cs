using UnityEngine;
using System.Collections;

public class Stat
{
    #region Constants

    public const float EMPTY_VALUE = 0f;

    #endregion

    #region Variables

    public float value     = 0f;
    public float increment = 0f;
    public float maxValue  = 1000;
    public bool isDisabled = false;

    #endregion

    #region Properties

    public bool IsEmpty
    {
        get { return this.value == EMPTY_VALUE; }
    }

    #endregion

    #region Constructors

    public Stat(float value)
    {
        this.value = value;
    }

    public Stat(bool isDisabled)
    {
        this.isDisabled = isDisabled;
    }

    #endregion

    #region Methods

    public void Update()
    {
        if(increment != 0 && !this.isDisabled)
            Increase(this.increment);
    }

    public void Decrease(float value)
    {
        this.value = Mathf.Clamp(this.value - value, EMPTY_VALUE, this.maxValue);
    }

    public void Increase(float value)
    {
        this.value = Mathf.Clamp(this.value + value, EMPTY_VALUE, this.maxValue);
    }

    #endregion
}
