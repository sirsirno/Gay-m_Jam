using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PassiveUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillTabUI tab;

    public string skillName;
    public Sprite skillIcon;
    public Property property;
    public string skillDetail;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tab.transform.DOKill();
        tab.transform.localScale = new Vector3(1f, 0.1f, 1);

        tab.nameText.text = skillName;
        tab.iconImg.sprite = skillIcon;

        if (property.Equals(Property.FIRE)) tab.propertyText.text = "<color=#ff0000>ºÒ</color>";
        else if (property.Equals(Property.WATER)) tab.propertyText.text = "<color=#0000ff>¹°</color>";

        tab.detailText.text = skillDetail;

        tab.transform.DOScaleY(1, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tab.transform.DOComplete();
        tab.transform.localScale = new Vector3(1f, 1f, 1);
        tab.transform.DOScaleY(0, 0.3f);
    }
}
