using UnityEngine;
using System.Collections;

public class Stat
{
    #region Variables

    public float value     = 0;
    public float increment = 0.05f;
    public float minValue  = 0;
    public float maxValue  = 1000;
    public bool isDisabled = false;

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
        if(increment != 0 && !isDisabled)
        {
            value += increment;
            value = Mathf.Clamp(value, minValue, maxValue);
        }
    }

    #endregion
}
