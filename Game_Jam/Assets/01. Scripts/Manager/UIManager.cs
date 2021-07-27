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
    [SerializeField] Image chain100Num;
    [SerializeField] Image chain10Num;
    [SerializeField] Image chain1Num;

    [SerializeField] RectTransform hpBar;

    [SerializeField] Image chainUI;

    [SerializeField] ParticleSystem chainParticle;
    Coroutine chainEndCoroutine;

    public int currentLeft;

    private void Start()
    {
        ChainNumRefresh();
    }

    private void Update()
    {
        SetHpFill();
    }

    public void SetHpFill()
    {
        hpBar.localScale = new Vector2(1, (float)GameManager.Instance.currentHp / GameManager.Instance.maxHp);
    }

    public void SetStageNumber(int stage)
    {
        if (stage > 9) return;
        stageNum.sprite = stageNumSprites[stage];
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
        currentLeft = leftNum;

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
        else
        {
            left1Num.gameObject.SetActive(true);
            left1Num.sprite = DefaultNumSprites[leftNum % 10];
        }
    }

    public void RefreshChainUI()
    {
        int chain = GameManager.Instance.chainCount;

        ChainNumRefresh();

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

    private void ChainNumRefresh()
    {
        int chain = GameManager.Instance.chainCount;

        chain100Num.gameObject.SetActive(false);
        chain10Num.gameObject.SetActive(false);
        chain1Num.gameObject.SetActive(false);

        if (chain >= 100)
        {
            chain100Num.gameObject.SetActive(true);
            chain100Num.sprite = DefaultNumSprites[chain / 100];

            chain10Num.gameObject.SetActive(true);
            chain10Num.sprite = DefaultNumSprites[(chain - ((chain / 100) * 100)) / 10];

            chain1Num.gameObject.SetActive(true);
            chain1Num.sprite = DefaultNumSprites[chain % 10];
        }
        else if (chain >= 10)
        {
            chain10Num.gameObject.SetActive(true);
            chain10Num.sprite = DefaultNumSprites[chain / 10];

            chain1Num.gameObject.SetActive(true);
            chain1Num.sprite = DefaultNumSprites[chain % 10];
        }
        else
        {
            chain1Num.gameObject.SetActive(true);
            chain1Num.sprite = DefaultNumSprites[chain % 10];
        }
    }

    IEnumerator ChainEnd()
    {
        yield return new WaitForSeconds(4f);
        chainUI.transform.DOScaleY(0, 0.5f);
        chainParticle.transform.DOScale(0, 0.5f);
        chainUI.DOFade(0, 0.5f);

    }
}
