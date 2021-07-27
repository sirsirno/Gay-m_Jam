using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill2 : Skill
{
    public float defaultDuration = 3f;
    private float duration = 3f;

    public GameObject firePrefab;

    [SerializeField] float[] FloorYs;

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
