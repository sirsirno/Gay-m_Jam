using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MoveObj
{
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnChangeProperty(Property prop)
    {
        if(prop.Equals(Property.FIRE))
        {
            animator.Play("Torch_On");
            SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Fire, 1);
            audioSource.Play();
        }
        else
        {
            animator.Play("Torch_Off");
            SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_FireOff, 1);
            audioSource.Stop();
        }
    }
}
