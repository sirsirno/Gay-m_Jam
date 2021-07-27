using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCircle : MoveObj
{
    protected override void OnChangeProperty(Property prop)
    {
        if (prop.Equals(Property.WATER))
        {
            SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_WaterBomb, 0.2f);
        }
    }
}
