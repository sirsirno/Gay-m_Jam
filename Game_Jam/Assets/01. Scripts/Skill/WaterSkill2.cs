using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaterSkill2 : Skill
{
    public float defaultDuration = 3f;
    private float duration = 3f;

    public GameObject padoPrefab;

    [SerializeField] float FloorX;
    [SerializeField] float[] FloorYs;
    [SerializeField] Transform[] waters;

    [SerializeField] Image gameBG;
    [SerializeField] Color waterColor;

    public override void Using()
    {
        isActive = true;
        duration = defaultDuration;

        for (int i = 0; i < 4; i++)
        {
            GameObject pado = PoolManager.GetItem<Effect_WaterPado>().gameObject;
            pado.transform.position = new Vector2(FloorX, FloorYs[i]);
        }

        foreach(Transform water in waters)
        {
            water.transform.DOScaleY(1, 1);
        }

        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Skill4Active, 1);
        gameBG.DOColor(waterColor, 0.5f).SetLoops(2, LoopType.Yoyo);
    }

    private void Start()
    {
        GameManager.Instance.skills[3] = this;
        PoolManager.CreatePool<Effect_WaterPado>(padoPrefab, transform, 4);
    }

    private void Update()
    {
        if (isActive)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                isActive = false;

                foreach (Transform water in waters)
                {
                    water.transform.DOScaleY(0, 1);
                }
            }
        }
    }
}

