using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_AddEnemy : MonoBehaviour
{
    public Torch torch = null;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Enemy enemy = coll.GetComponent<Enemy>();

        if (enemy != null)
        {
            torch.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Enemy enemy = coll.GetComponent<Enemy>();

        if (enemy != null)
        {
            torch.RemoveEnemy(enemy);
        }
    }
}
