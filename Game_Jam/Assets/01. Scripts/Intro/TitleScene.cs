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
    //[SerializeField] private Button skipBtn;
    [SerializeField] private Text startText;
    [SerializeField] private GameObject stageBackground;
    [SerializeField] private GameObject stages;
    [SerializeField] private GameObject Txts;
    [SerializeField] private Image cutSceneImg;
    [SerializeField] private List<GameObject> stageTxts = new List<GameObject>();
    [SerializeField] private List<Sprite> cutScene = new List<Sprite>();
    [SerializeField] private GameObject titlePlayer;
    private bool isTitle;
    private bool isCutScene;
    private int idx = 0;

    private float a = 1;

    public Image image;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        EventManager.AddEvent("gotoCutScene", () => { CutScene(); isCutScene = true;});
        EventManager.AddEvent("gotoTitle", () => TitleStart());
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isTitle)
        {
            optionUIManager.PauseBtnActiveSelf();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isCutScene)
        {
            OnClickSkipBtn();
        }
        if (Input.GetMouseButtonDown(0) && idx < 5 && isCutScene)
        {
            idx++;
            if (idx <= 4) 
            {
                cutSceneImg.GetComponent<Image>().sprite = cutScene[idx];
            }
        }
        else if (idx > 4 && isCutScene) 
        {
            isCutScene = false;
            cutSceneImg.gameObject.SetActive(false);
            EventManager.Invoke("gotoTitle");
        }

    }
    void CutScene() 
    {
        StartCoroutine(FadeOut());
        cutSceneImg.gameObject.SetActive(true);
        cutSceneImg.GetComponent<Image>().sprite = cutScene[0];
      //  skipBtn.gameObject.SetActive(true);
       // skipBtn.onClick.AddListener(() => OnClickSkipBtn());

        
    }
    void OnClickSkipBtn() 
    {
        isCutScene = false;
        cutSceneImg.gameObject.SetActive(false);
       // skipBtn.gameObject.SetActive(false);
        EventManager.Invoke("gotoTitle");
    }
    void TitleStart()
    {
        Debug.Log("여기서부턴 타이틀 애니");
        isTitle = true;
        startButton.gameObject.SetActive(true);
        startButton.onClick.AddListener(() => { ClickToStart(); });
        startText.DOColor(new Color(1f, 1f, 1f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);

        audioSource.Play();
    }

    public void ClickToStart() 
    {
        isTitle = false;
        startText.gameObject.SetActive(false);
        stages.SetActive(true);
        stageBackground.SetActive(true);
        stages.transform.DOLocalMoveX(600f, 1f);
        titlePlayer.SetActive(false);
        Txts.SetActive(true);
        stageTxts[0].GetComponent<Button>().onClick.AddListener(() => { OnClickInGameBtn(); });
        stageTxts[0].transform.DOLocalMoveX(-50f, 0.5f);
        stageTxts[1].transform.DOLocalMoveX(-50f, 1.5f);
        stageTxts[2].transform.DOLocalMoveX(-50f, 2.5f);
        stageTxts[3].transform.DOLocalMoveX(-50f, 3.5f);
    }
    void OnClickInGameBtn() 
    {
        SceneController.LoadScene("InGame");
    }
    public IEnumerator FadeIn()
    {

        while (true)
        {
            a += 0.01f;
            image.color = new Color(0, 0, 0, a);
            Debug.Log(a);
            yield return new WaitForSeconds(0.01f);
            if (a >= 1)
                break;
        }

        //StartCoroutine(FadeOut());
    }
    public IEnumerator FadeOut()
    {
        while (true)
        {
            a -= 0.01f;
            image.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
            if (a <= 0)
                break;
        }

    }
}
