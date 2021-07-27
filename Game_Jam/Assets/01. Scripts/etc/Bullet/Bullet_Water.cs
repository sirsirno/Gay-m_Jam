using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Water : Effect, IDamage
{
    [SerializeField] private int damage = 3;
    public Transform targetTransform;
    public float speed;

    private GameObject targetObj;

    private void Update()
    {
        if(!targetTransform.gameObject.activeSelf)
        {
            for (int i = 0; i < GameManager.Instance.enemyList.Count; i++)
            {
                if (targetObj != null)
                {
                    float originDis = Vector2.Distance(transform.position, targetObj.transform.position);
                    float newDis = Vector2.Distance(transform.position, GameManager.Instance.enemyList[i].transform.position);
                    if (originDis > newDis)
                    {
                        targetObj = GameManager.Instance.enemyList[i].gameObject;
                    }
                }
                else targetObj = GameManager.Instance.enemyList[i].gameObject;
            }

            targetTransform = targetObj.transform;
        }


        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

        Vector2 dir = targetTransform.position - transform.position;
        float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, degree - 90);
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }
}
