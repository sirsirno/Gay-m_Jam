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
        PlayerProperty player = Instantiate(playerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerProperty>();

        player.Init(transform.position, this as IInteractable);
    }

    protected override void OnChangeProperty(Property prop)
    {
        // 이펙트 추가점
    }
}
