using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public void Fade(float value, float duration, TweenCallback action = null)
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        TrailRenderer[] trails = GetComponentsInChildren<TrailRenderer>();

        for (int i = 0; i< sprites.Length;i++)
        {
            if (i == sprites.Length - 1 && action != null)
            {
                sprites[i].DOFade(value, duration).OnComplete(action);
            }
            else
            {
                sprites[i].DOFade(value, duration);
            }
        }

        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].DOTime(value, duration);
        }
    }
}
