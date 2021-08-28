using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGUIHandler : MonoBehaviour
{
    [Header("��� �̹���")]
    public List<Sprite> bgImage = new List<Sprite>();

    [Header("�� �̹���")]
    public List<Sprite> groundImage = new List<Sprite>();

    public Image bg;
    public Image ground;

    void Start()
    {
        bg.sprite = bgImage[GameManager.Instance.stage];
        ground.sprite = groundImage[GameManager.Instance.stage];
    }
}
