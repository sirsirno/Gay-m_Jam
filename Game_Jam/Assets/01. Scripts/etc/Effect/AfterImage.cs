using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void SetSprite(Sprite sprite, bool flip, Vector3 position)
    {
        transform.position = position;
        spriteRenderer.flipX = flip;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); 
        spriteRenderer.sprite = sprite;

        spriteRenderer.DOFade(0, 0.7f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}