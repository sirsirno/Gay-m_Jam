using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    PlayerProperty playerProperty;
    SpriteRenderer sr;

    [SerializeField] LineRenderer interactiveLine;
    [SerializeField] Texture2D lineTex;

    [Header("Particles")]
    [SerializeField] ParticleSystem[] idleParticles;
    [SerializeField] ParticleSystem bombParticle;
    private float[] idleParticlesAlpha;
    private float bombParticleAlpha;

    private void Awake()
    {
        playerProperty = GetComponent<PlayerProperty>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        interactiveLine.material.mainTexture = lineTex;
        PlayParticle(ParticleType.IDLE);

        idleParticlesAlpha = new float[idleParticles.Length];
        for (int i = 0; i< idleParticles.Length;i++)
        {
            idleParticlesAlpha[i] = idleParticles[i].main.startColor.color.a;
        }
        bombParticleAlpha = bombParticle.main.startColor.color.a;
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

    public void SetAlphaValue(float alpha)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        for (int i = 0; i < idleParticles.Length; i++)
        {
            Color idleColor = idleParticles[i].main.startColor.color;
            var idleMain = idleParticles[i].main;
            idleMain.startColor = new Color(idleColor.r, idleColor.g, idleColor.b, idleParticlesAlpha[i] * alpha);
        }

        Color bombColor = bombParticle.main.startColor.color;
        var bombMain = bombParticle.main;
        bombMain.startColor = new Color(bombColor.r, bombColor.g, bombColor.b, bombParticleAlpha * alpha);
    }
}
