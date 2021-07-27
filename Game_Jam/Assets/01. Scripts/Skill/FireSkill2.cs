using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireSkill2 : Skill
{
    public float defaultDuration = 3f;
    private float duration = 3f;

    public GameObject firePrefab;

    [SerializeField] float[] FloorYs;

    [SerializeField] Image gameBG;
    [SerializeField] Color fireColor;

    public override void Using()
    {
        isActive = true;
        duration = defaultDuration;

        for (int i = 0; i < 4; i++)
        {
            GameObject fire = PoolManager.GetItem<Effect_FireSkill2>().gameObject;
            Transform firePlayer = GameManager.Instance.cameraHandler.fireTransform;
            fire.transform.position = new Vector2(firePlayer.position.x, FloorYs[i]);
        }

        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Skill2Active, 1);
        gameBG.DOColor(fireColor, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    private void Start()
    {
        GameManager.Instance.skills[1] = this;
        PoolManager.CreatePool<Effect_FireSkill2>(firePrefab, transform, 4);
    }

    private void Update()
    {
        if (isActive)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                isActive = false;

            }
        }
    }
}
