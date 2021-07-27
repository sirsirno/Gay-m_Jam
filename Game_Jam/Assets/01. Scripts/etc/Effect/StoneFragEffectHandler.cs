using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFragEffectHandler : MonoBehaviour
{
    public GameObject particle;

    private void Start()
    {
        PoolManager.CreatePool<Effect_StoneFrag>(particle, transform, 5);        
    }

    public static void CreateStoneFrag(Vector3 position)
    {
        Effect_StoneFrag effect = PoolManager.GetItem<Effect_StoneFrag>();

        effect.transform.position = position;
    }
}
