using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairScript : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Enemy enemy = coll.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
        }
    }
}
