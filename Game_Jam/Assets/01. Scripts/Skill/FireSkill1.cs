using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill1 : Skill
{
    public float defaultDuration = 15f;
    private float duration = 15f;

    private bool isActive = false;

    public bool IsActive { get { return isActive; } }

    public override void Using()
    {
        isActive = true;
        duration = defaultDuration;
    }

    private void Update()
    {
        if(isActive)
        {


            duration -= Time.deltaTime;

            if(duration <= 0)
            {
                isActive = false;
            }
        }
    }
}
