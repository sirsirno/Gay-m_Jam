using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null;

        for (int i = 0; i < GameManager.Instance.playerList.Count; i++)
        {
            if (GameManager.Instance.playerList[i].MyProperty == Property.FIRE)
            {
                virtualCamera.Follow = GameManager.Instance.playerList[i].transform;
            }
        }
    }
}
