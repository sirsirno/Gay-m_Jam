using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("BGM")]
    public AudioClip Audio_BGM;

    [Header("SFX")]
    public AudioClip Audio_SFX_Fire;
    public AudioClip Audio_SFX_FireOff;
    public AudioClip Audio_SFX_Water;
    public AudioClip Audio_SFX_Golem;
    public AudioClip Audio_SFX_UI;
    public AudioClip Audio_SFX_StoneHit;
    public AudioClip Audio_SFX_Pipe;
    public AudioClip Audio_SFX_StoneBreak;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBGMSound(AudioClip clip, float volume)
    {
        BGMSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXSound(AudioClip clip, float volume)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}
