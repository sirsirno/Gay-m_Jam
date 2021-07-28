using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameClearHandler : MonoBehaviour
{
    public CanvasGroup bgPanel;
    public GameObject gameClearPanel;

    [Header("파티클")]
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

    [Header("버튼들")]
    public Button gotoMainBtn;

    private void Start()
    {
        bgPanel.alpha = 0;
        gameClearPanel.transform.localScale = Vector3.zero;

        bgPanel.gameObject.SetActive(false);
        gameClearPanel.gameObject.SetActive(false);

        gotoMainBtn.onClick.AddListener(() => {
            GameManager.Instance.ResetValue();
            PoolManager.ResetPool();
            SceneController.LoadScene("Title");
        });

        //OnGameClear();
        EventManager.AddEvent("OnGameClear", OnGameClear);
    }

    void OnGameClear()
    {
        bgPanel.gameObject.SetActive(true);
        gameClearPanel.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence()
            .Append(bgPanel.DOFade(1f, 1f))
            .AppendCallback(() =>
            {
                leftParticle.Play();
                rightParticle.Play();
            })
            .Join(gameClearPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
    }

}
