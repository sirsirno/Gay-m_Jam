using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill1 : Skill
{
    public float defaultDuration = 15f;
    private float duration = 15f;

    public override void Using()
    {
        isActive = true;
        duration = defaultDuration;
        GameManager.Instance.QDefault.defaultAttack.SetActive(false);
        GameManager.Instance.QDefault.QAttack.SetActive(true);
        GameManager.Instance.QDefault.speed = 600f;

    }

    private void Start()
    {
        GameManager.Instance.skills[0] = this;
    }

    private void Update()
    {
        if(isActive)
        {
            duration -= Time.deltaTime;

            if(duration <= 0)
            {
                isActive = false;
                GameManager.Instance.QDefault.defaultAttack.SetActive(true);
                GameManager.Instance.QDefault.QAttack.SetActive(false);
                GameManager.Instance.QDefault.speed = 400f;
            }
        }
    }
}
