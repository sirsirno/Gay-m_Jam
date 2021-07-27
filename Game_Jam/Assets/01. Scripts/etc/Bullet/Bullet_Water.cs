using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Water : Effect, IDamage
{
    [SerializeField] private int damage = 3;
    public Transform targetTransform;
    public float speed;

    private void Update()
    {
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
