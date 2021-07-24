using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroScene : MonoBehaviour
{
    public GameObject mintFlowObj = null;
    public RectTransform chocoFlowObj = null;

    public CanvasGroup logoCanvasGroup = null;

    [Header("오디오 관련")]
    public AudioSource sfxManager;
    public AudioClip Audio_Trumpet;
    public AudioClip Audio_WaterPour;

    [Header("파티클")]
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

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
        yield return new WaitForSeconds(1f);
        sfxManager.PlayOneShot(Audio_WaterPour, 1f);
        yield return new WaitForSeconds(2);

        DOTween.To(() => logoCanvasGroup.alpha, value => logoCanvasGroup.alpha = value, 1, 0.5f);

        Sequence seq = DOTween.Sequence();

        sfxManager.PlayOneShot(Audio_Trumpet, 1f);
        leftParticle.Play();
        rightParticle.Play();

        seq.Append(logoCanvasGroup.transform.DOScale(1, 0.75f).SetEase(Ease.OutBack));
        seq.Append(chocoFlowObj.DOAnchorPosY(-442, 3).SetEase(Ease.OutCirc));
        seq.Append(DOTween.To(() => logoCanvasGroup.alpha, value => logoCanvasGroup.alpha = value, 0, 0.5f).OnComplete(() =>
        {
            EventManager.Invoke("gotoTitle");
        }));
    }
}