using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private PlayerProperty playerProperty = null;

    private void Start()
    {
        playerProperty = GetComponent<PlayerProperty>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerProperty pp = coll.GetComponent<PlayerProperty>();

        if (pp != null)
        {
            if (!pp.MyProperty.Equals(playerProperty))
            {
                // µÚÁ³¤¢´Ù
                print("µÚ1Áü");
            }
        }
    }
}
