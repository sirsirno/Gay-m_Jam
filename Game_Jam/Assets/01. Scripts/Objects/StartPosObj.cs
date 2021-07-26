using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosObj : InteractableObj
{
    [SerializeField] private GameObject playerPrefab = null;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }

    protected override void OnChangeProperty(Property prop)
    {
        // 이펙트 추가점
    }
}
