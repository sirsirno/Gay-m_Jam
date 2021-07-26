using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    PlayerProperty playerProperty;

    [SerializeField] LineRenderer interactiveLine;
    [SerializeField] Texture2D lineTex;

    [Header("Particles")]
    [SerializeField] ParticleSystem[] idleParticles;
    [SerializeField] ParticleSystem bombParticle;

    private void Awake()
    {
        playerProperty = GetComponent<PlayerProperty>();
    }

    private void Start()
    {
        interactiveLine.material.mainTexture = lineTex;
        PlayParticle(ParticleType.IDLE);
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
                StopParticle();
            }
        }
        else
        {
            if (interactiveLine.gameObject.activeSelf)
            {
                interactiveLine.gameObject.SetActive(false);
                PlayParticle(ParticleType.IDLE);
            }
        }
    }

    public void PlayParticle(ParticleType type)
    {
        if (type == ParticleType.IDLE)
        {
            foreach (ParticleSystem idle in idleParticles)
            {
                idle.Play();
            }
        }
        else if (type == ParticleType.BOMB)
        {
            bombParticle.Play();
        }
    }

    public void StopParticle() // Idle만 Looping이므로 idle만 스탑해준다
    {
        foreach (ParticleSystem idle in idleParticles)
        {
            idle.Stop();
        }
    }
}
