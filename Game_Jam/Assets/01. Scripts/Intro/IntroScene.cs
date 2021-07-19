using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroScene : MonoBehaviour
{
    public GameObject mintFlowObj = null;
    public RectTransform chocoFlowObj = null;

    public CanvasGroup logoCanvasGroup = null;

    private void Awake()
    {
        logoCanvasGroup.alpha = 0;
        logoCanvasGroup.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(1f);
        mintFlowObj.SetActive(true);
        yield return new WaitForSeconds(3);

        DOTween.To(() => logoCanvasGroup.alpha, value => logoCanvasGroup.alpha = value, 1, 0.5f);

        Sequence seq = DOTween.Sequence();

        seq.Append(logoCanvasGroup.transform.DOScale(1, 0.75f).SetEase(Ease.OutBack));
        seq.Append(chocoFlowObj.DOAnchorPosY(-442, 3).SetEase(Ease.OutCirc));
        seq.Append(DOTween.To(() => logoCanvasGroup.alpha, value => logoCanvasGroup.alpha = value, 0, 0.5f).OnComplete(() =>
        {
            EventManager.Invoke("gotoTitle");
        }));
    }
}