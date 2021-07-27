using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PreviewUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite previewSpr;
    public GameObject previewPanel;
    public Image previewImg;

    public void OnPointerEnter(PointerEventData eventData)
    {
        previewImg.sprite = previewSpr;
        previewPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (previewImg.sprite == previewSpr)
        {
            previewPanel.SetActive(false);
        }
    }
}
