using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] Image chainUI;
    [SerializeField] ParticleSystem chainParticle;
    Coroutine chainEndCoroutine;

    private void Start()
    {
        SetWaveNumber(15);
        SetLeftNumber(123);
    }

    private void Update()
    {
        SetHpFill();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RefreshChainUI();
        }
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

    public void RefreshChainUI()
    {
        chainUI.transform.DOKill();
        chainParticle.transform.DOKill();
        chainUI.DOKill();

        chainUI.transform.localScale = new Vector2(0, 1);
        chainUI.color = new Color(1, 1, 1, 0);

        chainUI.transform.DOScaleX(1, 0.25f);
        chainParticle.transform.DOScale(1, 0.25f);
        chainUI.DOFade(1, 0.25f);

        if (chainEndCoroutine != null)
        {
            StopCoroutine(chainEndCoroutine);
        }
        chainEndCoroutine = StartCoroutine(ChainEnd());
    }

    IEnumerator ChainEnd()
    {
        yield return new WaitForSeconds(4f);
        chainUI.transform.DOScaleY(0, 0.5f);
        chainParticle.transform.DOScale(0, 0.5f);
        chainUI.DOFade(0, 0.5f);
    }
}
