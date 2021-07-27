using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    PlayerProperty playerProperty = null;

    public float attackCoolTime = 0.5f;
    private float attackCoolTimeCur = 0f;

    [SerializeField] private Projectile attackObj;

    private void Awake()
    {
        playerProperty = GetComponent<PlayerProperty>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && playerProperty.MyProperty == GameManager.Instance.currentProperty/*&& !playerProperty.IsMoving*/)
        {
            attackCoolTimeCur += Time.deltaTime;

            if (attackCoolTimeCur >= attackCoolTime)
            {
                Attack();
                attackObj.Create();
                attackCoolTimeCur = 0.01f;
            }
        }
        else
        {
            if (attackCoolTimeCur != 0f)
            {
                attackCoolTimeCur = 0f;
                attackObj.transform.DOKill();
                attackObj.Fade(0, 0.5f, () => attackObj.gameObject.SetActive(false));
            }
        }
    }

    private void Attack()
    {
        if (!attackObj.gameObject.activeSelf)
        {
            attackObj.gameObject.SetActive(true);
            attackObj.Fade(1, 0.5f);
        }
    }
}
