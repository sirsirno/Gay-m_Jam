using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    [Header("BGM")]
    public AudioClip Audio_BGM_InGame;

    [Header("SFX")]
    public AudioClip Audio_SFX_Fire;
    public AudioClip Audio_SFX_FireOff;
    public AudioClip Audio_SFX_Water;
    public AudioClip Audio_SFX_WaterBomb;
    public AudioClip Audio_SFX_WaterHit;
    public AudioClip Audio_SFX_Golem;
    public AudioClip Audio_SFX_Golem_HitGround;
    public AudioClip Audio_SFX_Golem_Punch1;
    public AudioClip Audio_SFX_Golem_Punch2;
    public AudioClip Audio_SFX_StoneHit;
    public AudioClip Audio_SFX_Pipe;
    public AudioClip Audio_SFX_StoneBreak;
    public AudioClip Audio_SFX_Deny;
    public AudioClip Audio_SFX_PauseOff;
    public AudioClip Audio_SFX_Skill1Active;
    public AudioClip Audio_SFX_Skill2Active;
    public AudioClip Audio_SFX_Skill3Active;
    public AudioClip Audio_SFX_Skill3Bubble;
    public AudioClip Audio_SFX_Skill4Active;

    public AudioClip Audio_SFX_UI;
    public AudioClip Audio_SFX_WaveStart;
    public AudioClip Audio_SFX_GameClear;
    public AudioClip Audio_SFX_GameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayBGMSound(Audio_BGM_InGame, 0.15f);
    }

    public void PlayBGMSound(AudioClip clip, float volume)
    {
        BGMSource.clip = clip;
        BGMSource.volume = volume;
        BGMSource.Play();
    }

    public void PlaySFXSound(AudioClip clip, float volume)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}
