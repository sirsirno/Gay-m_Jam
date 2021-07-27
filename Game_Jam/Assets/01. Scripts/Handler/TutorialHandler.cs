using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialHandler : MonoBehaviour
{
    private CanvasGroup cg = null;

    [Header("����â")]
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

        ShowText("�������ڿ� ���Ű��� ȯ���մϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���ݺ��� ����� ȭ�鿡 ���̽ô� �� ���Ҹ� Ȱ���Ͽ�\n�������� ������ �����ľ� �մϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���Ҵ� WASD�� ����Ͽ� �� ���� ������ �� �ֽ��ϴ�.", 1.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ���Ҹ� �巡�� ����Ͽ� �ٷ� ����Ǿ� ���� ���� ������\n�̵��� �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���Ҹ� �巡�� ����ϴ� ���߿��� ���鿡�� ���ظ� �ִ� ������ �մϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� Ư���� ���� ���� ���Ұ� ���� ��\n���ǰ� ��ȣ�ۿ��� �Ͽ� Ư���� ȿ���� ��Ÿ���ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ��� ȶ�� ���� ���� ���� �� ȶ�ҿ� ���� �پ� �ֺ��� ���� �����մϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ��ų�� ���ؼ� �����ϰڽ��ϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� �ϴܿ� ���̽ô� â�� ��ųâ�̸�\n���콺�� �����ٴ�� ������ ��ų������ ���ɴϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("��ų�� ��ų��ư�� ���� �����ų�\n����Ű�� �̿��Ͽ� ��� �� �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("��ų�� ����Ϸ��� �ڽ�Ʈ�� �����ؾ� �ϸ�\n�ڽ�Ʈ�� ���� óġ�ϸ� ���̰� �˴ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("1�� �߾ӿ��� ������ ��Ȳ���� Ż �� �ִ� ���� �ֽ��ϴ�.", 1.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ź ���¿����� A,D�� �̵��� �� �ְ�\n�����̽��ٷ� ������ �� �� ������\nW,S�� ���鿡�� ������ ������ �� �� �ֽ��ϴ�.", 2.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("�׷� ����� ���ϴ�!", 1f);
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
