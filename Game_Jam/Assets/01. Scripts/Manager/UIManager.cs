using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Sprite[] stageNumSprites; 
    [SerializeField] Sprite[] DefaultNumSprites;

    [SerializeField] Image stageNum;
    [SerializeField] Image wave10Num;
    [SerializeField] Image wave1Num;
    [SerializeField] Image left100Num;
    [SerializeField] Image left10Num;
    [SerializeField] Image left1Num;

    [SerializeField] RectTransform hpBar;

    private void Start()
    {
        SetWaveNumber(15);
        SetLeftNumber(123);
    }

    private void Update()
    {
        SetHpFill();
    }

    public void SetHpFill()
    {
        hpBar.localScale = new Vector2(1, (float)GameManager.Instance.currentHp / GameManager.Instance.maxHp);
    }

    public void SetWaveNumber(int waveNum)
    {
        wave10Num.gameObject.SetActive(false);
        wave1Num.gameObject.SetActive(false);

        if (waveNum >= 10)
        {
            wave10Num.gameObject.SetActive(true);
            wave10Num.sprite = DefaultNumSprites[waveNum / 10];
        }

        wave1Num.gameObject.SetActive(true);
        wave1Num.sprite = DefaultNumSprites[waveNum % 10];
    }

    public void SetLeftNumber(int leftNum)
    {
        left100Num.gameObject.SetActive(false);
        left10Num.gameObject.SetActive(false);
        left1Num.gameObject.SetActive(false);

        if (leftNum >= 100)
        {
            left100Num.gameObject.SetActive(true);
            left100Num.sprite = DefaultNumSprites[leftNum / 100];

            left10Num.gameObject.SetActive(true);
            left10Num.sprite = DefaultNumSprites[(leftNum - ((leftNum / 100) * 100)) / 10];

            left1Num.gameObject.SetActive(true);
            left1Num.sprite = DefaultNumSprites[leftNum % 10];
        }
        else if (leftNum >= 10)
        {
            left10Num.gameObject.SetActive(true);
            left10Num.sprite = DefaultNumSprites[leftNum / 10];

            left1Num.gameObject.SetActive(true);
            left1Num.sprite = DefaultNumSprites[leftNum % 10];
        }
    }
}
