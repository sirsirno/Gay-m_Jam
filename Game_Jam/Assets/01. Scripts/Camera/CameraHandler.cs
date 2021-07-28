using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    CinemachineImpulseSource source;
    bool isFireFollow = true; // true면 불 false면 물

    public Transform fireTransform;
    public Transform waterTransform;

    public GameObject fireSelect;
    public GameObject waterSelect;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        source = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        GameManager.Instance.cameraHandler = this;
        StartCoroutine(Init());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isFireFollow = !isFireFollow;

            if(isFireFollow)
            {
                ChangeCurrentPlayer(Property.FIRE);
            }
            else
            {
                ChangeCurrentPlayer(Property.WATER);
            }
        }
    }

    public void ChangeCurrentPlayer(Property property)
    {
        if (property == Property.FIRE)
        {
            virtualCamera.Follow = fireTransform;
            fireSelect.SetActive(true);
            waterSelect.SetActive(false);
            isFireFollow = true;
            GameManager.Instance.currentProperty = Property.FIRE;
        }
        else if (property == Property.WATER)
        {
            virtualCamera.Follow = waterTransform;
            fireSelect.SetActive(false);
            waterSelect.SetActive(true);
            isFireFollow = false;
            GameManager.Instance.currentProperty = Property.WATER;
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
                fireSelect.SetActive(true);
                waterSelect.SetActive(false);
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

    public void CameraImpulse(float force)
    {
        source.GenerateImpulse(force);
    }
}
