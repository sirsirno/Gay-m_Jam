using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SkillTabUI tab;
    public Skill skill;

    public string skillName;
    public Sprite skillIcon;
    public int chainCost;
    public Property property;
    public string skillDetail;
    public Text skillCostValue;

    [Space(2)]
    public Image coolTimeImg;
    public float coolTime;
    public float currentCoolTime = 0;
    public KeyCode keyCode;

    private void Start()
    {
        skillCostValue.text = chainCost.ToString();
    }

    private void Update()
    {
        if (currentCoolTime > 0)
        {
            currentCoolTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(keyCode))
        {
            if(currentCoolTime <= 0)
            {
                if (GameManager.Instance.chainCount < chainCost)
                {
                    GameManager.Instance.uiManager.FadeRedChainNum();
                }
                else
                {
                    GameManager.Instance.chainCount -= chainCost;
                    currentCoolTime = coolTime;
                    skill.Using();
                }
            }
            else
            {
                coolTimeImg.DOComplete();
                coolTimeImg.color = Color.red;
                coolTimeImg.DOColor(new Color(0, 0, 0, 192f / 255), 0.3f);
            }
        }

        coolTimeImg.fillAmount = currentCoolTime / coolTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tab.transform.DOKill();

        tab.transform.localScale = new Vector3(0.1f, 0.1f, 1);

        tab.nameText.text = skillName;
        tab.iconImg.sprite = skillIcon;
        tab.costText.text = chainCost.ToString();

        if (property.Equals(Property.FIRE)) tab.propertyText.text = "<color=#ff0000>阂 加己</color>";
        else if (property.Equals(Property.WATER)) tab.propertyText.text = "<color=#0000ff>拱 加己</color>";

        tab.detailText.text = skillDetail;

        tab.transform.DOScaleX(1, 0.2f).OnComplete(() =>
        {
            tab.transform.DOScaleY(1, 0.3f);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tab.transform.DOComplete();
        tab.transform.localScale = new Vector3(1f, 1f, 1);
        tab.transform.DOScaleY(0, 0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentCoolTime <= 0)
        {
            if (GameManager.Instance.chainCount < chainCost)
            {
                GameManager.Instance.uiManager.FadeRedChainNum();
            }
            else
            {
                GameManager.Instance.chainCount -= chainCost;
                currentCoolTime = coolTime;
                skill.Using();
            }
        }
        else
        {
            coolTimeImg.DOComplete();
            coolTimeImg.color = Color.red;
            coolTimeImg.DOColor(new Color(0, 0, 0, 192f / 255),0.3f);
        }
    }
}
