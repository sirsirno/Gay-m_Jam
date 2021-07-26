using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    PlayerProperty playerProperty;
    PlayerInput playerInput;

    [SerializeField] LineRenderer interactiveLine;
    [SerializeField] Texture2D lineTex;

    private void Awake()
    {
        playerProperty = GetComponent<PlayerProperty>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        interactiveLine.material.mainTexture = lineTex;
    }

    private void Update()
    {
        if (playerProperty.IsMoving)
        {
            if (interactiveLine.gameObject.activeSelf)
            {
                interactiveLine.SetPosition(0, playerProperty.CurrentPos);
                interactiveLine.SetPosition(1, playerProperty.TargetLerp);
            }
            else
            {
                interactiveLine.gameObject.SetActive(true);
                interactiveLine.SetPosition(0, playerProperty.CurrentPos);
                interactiveLine.SetPosition(1, playerProperty.TargetLerp);
            }
        }
        else
        {
            if (interactiveLine.gameObject.activeSelf)
            {
                interactiveLine.gameObject.SetActive(false);
            }
        }
    }
}
