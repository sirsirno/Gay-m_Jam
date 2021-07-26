using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    CinemachineImpulseSource source;
    bool isFireFollow = true; // true면 불 false면 물

    Transform fireTransform;
    Transform waterTransform;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isFireFollow = !isFireFollow;

            if(isFireFollow)
            {
                virtualCamera.Follow = fireTransform;
            }
            else
            {
                virtualCamera.Follow = waterTransform;
            }
        }
    }

    IEnumerator Init()
    {
        yield return null;

        for (int i = 0; i < GameManager.Instance.playerList.Count; i++)
        {
            if (GameManager.Instance.playerList[i].MyProperty == Property.FIRE)
            {
                fireTransform = GameManager.Instance.playerList[i].transform;
                virtualCamera.Follow = GameManager.Instance.playerList[i].transform;
            }
            else if (GameManager.Instance.playerList[i].MyProperty == Property.WATER)
            {
                waterTransform = GameManager.Instance.playerList[i].transform;
            }
        }

        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
        virtualCamera.GetComponent<CinemachineConfiner>().m_Damping = 0;
        yield return new WaitForSeconds(0.1f);
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0.5f;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0.5f;
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0.5f;
        virtualCamera.GetComponent<CinemachineConfiner>().m_Damping = 0.5f;
    }
}
