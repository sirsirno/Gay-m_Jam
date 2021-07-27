using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFragDeadEffectHandler : MonoBehaviour
{
    public GameObject particle;

    private void Start()
    {
        PoolManager.CreatePool<Effect_StoneFragDead>(particle, transform, 5);        
    }

    public static void CreateStoneFrag(Vector3 position)
    {
        Effect_StoneFragDead effect = PoolManager.GetItem<Effect_StoneFragDead>();

        effect.transform.position = position;
    }
}
