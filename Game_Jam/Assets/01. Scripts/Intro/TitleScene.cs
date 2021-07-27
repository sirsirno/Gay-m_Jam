using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScene : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] OptionUIManager optionUIManager;
    [SerializeField] private Button startButton;
    [SerializeField] private Text startText;
    [SerializeField] private GameObject stageBackground;
    [SerializeField] private GameObject stages;
    [SerializeField] private GameObject Txts;
    [SerializeField] private List<GameObject> stageTxts = new List<GameObject>();
    private bool isTitle;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        EventManager.AddEvent("gotoTitle", () => TitleStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& isTitle) 
        {
            optionUIManager.PauseBtnActiveSelf();
        }
      
    }

    void TitleStart()
    {
        Debug.Log("여기서부턴 타이틀 애니");
        isTitle = true;
        startButton.gameObject.SetActive(true);
        startButton.onClick.AddListener(() => { ClickToStart(); });
        startText.DOColor(new Color(0f, 0f, 0f, 0f), 2f).SetLoops(-1, LoopType.Yoyo);

        audioSource.Play();
    }

    void ClickToStart() 
    {
        isTitle = false;
        startText.gameObject.SetActive(false);
        stages.SetActive(true);
        stageBackground.SetActive(true);
        stages.transform.DOLocalMoveX(600f, 1f);
        Txts.SetActive(true);
        stageTxts[0].transform.DOLocalMoveX(-50f, 1f);
        stageTxts[1].transform.DOLocalMoveX(-50f, 2f);
        stageTxts[2].transform.DOLocalMoveX(-50f, 3f);
        stageTxts[3].transform.DOLocalMoveX(-50f, 4f);
    }
}
