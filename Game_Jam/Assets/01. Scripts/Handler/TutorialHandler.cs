using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialHandler : MonoBehaviour
{
    private CanvasGroup cg = null;

    [Header("설명창")]
    [SerializeField] private Text tutorialText = null;
    [SerializeField] private Image skipImg = null;

    private string currentText;

    private bool isText = false;

    private bool isTextEnd = false;
    private bool isFinished = false;

    private Tweener textTween = null;

    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds pFiveSecWait = new WaitForSeconds(0.5f);

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();

        cg.alpha = 0;
        tutorialText.text = " ";

        skipImg.enabled = false;
        skipImg.transform.DOLocalMoveY(skipImg.transform.localPosition.y + 10f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(Tutorial());
    }

    private void Update()
    {
        SkipText();
    }

    private IEnumerator Tutorial()
    {
        HidePanel(false, 2f);
        yield return oneSecWait;
        yield return oneSecWait;

        ShowText("마가보자에 오신것을 환영합니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("지금부터 당신은 화면에 보이시는 두 원소를 활용하여\n몰려오는 적들을 물리쳐야 합니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("원소는 WASD를 사용하여 줄 위를 움직일 수 있습니다.", 1.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("또한 원소를 드래그 드랍하여 줄로 연결되어 있지 않은 곳으로\n이동할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("원소를 드래그 드랍하는 도중에는 적들에게 피해를 주는 공격을 합니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("또한 특정한 물건 위에 원소가 있을 시\n물건과 상호작용을 하여 특별한 효과가 나타납니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("예를 들어 횃불 위에 불이 있을 시 횃불에 불이 붙어 주변의 적을 공격합니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("이제 스킬에 대해서 설명하겠습니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("우측 하단에 보이시는 창이 스킬창이며\n마우스를 가져다대면 각각의 스킬설명이 나옵니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("스킬은 스킬버튼을 직접 누르거나\n숫자키를 이용하여 사용 할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("스킬을 사용하려면 코스트를 지불해야 하며\n코스트는 적을 처치하면 쌓이게 됩니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("1층 중앙에는 위급한 상황에서 탈 수 있는 골렘이 있습니다.", 1.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("골렘을 탄 상태에서는 A,D로 이동할 수 있고\n스페이스바로 점프를 할 수 있으며\nW,S로 적들에게 강력한 공격을 할 수 있습니다.", 2.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("그럼 행운을 빕니다!", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        HidePanel(true, 2f);
        yield return oneSecWait;
        yield return oneSecWait;

        EventManager.Invoke("OnGameStart");
        gameObject.SetActive(false);
    }

    private void ShowText(string text, float dur = 1f)
    {
        skipImg.enabled = false;

        isText = true;
        isTextEnd = false;

        currentText = text;

        tutorialText.text = "";
        textTween = tutorialText.DOText(text, dur)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => 
                    { 
                        isTextEnd = true;
                        skipImg.enabled = true;
                    });
    }

    private void HidePanel(bool isHide, float dur = 1f)
    {
        if (isHide)
        {
            cg.DOFade(0f, dur);
        }
        else
        {
            cg.DOFade(1f, dur);
        }
    }

    private void SkipText()
    {
        if (!isText) return;

        if (!isTextEnd && Input.GetMouseButtonUp(0))
        {
            isTextEnd = true;
            skipImg.enabled = true;

            textTween.Kill();
            tutorialText.text = currentText;
        }
        else if (isTextEnd && Input.GetMouseButtonUp(0))
        {
            isText = false;
            isFinished = true;
        }
    }
}
