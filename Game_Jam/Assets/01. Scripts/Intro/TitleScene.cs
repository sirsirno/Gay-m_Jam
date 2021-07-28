using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScene : MonoBehaviour
{
    AudioSource BGMAudioSource;

    [SerializeField] private AudioSource SFXAudioSource;

    [Header("BGM")]
    [SerializeField] AudioClip Audio_BGM_Legend;
    [SerializeField] AudioClip Audio_BGM_Title;

    private bool isCutScene;
    [SerializeField] private Image cutSceneImg;
    [SerializeField] private List<Sprite> cutScene = new List<Sprite>();
    [SerializeField] private GameObject skipText;
    private Coroutine cutSceneCoroutine;

    private bool isTitle;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private Text startText;
    [SerializeField] private Transform titleImg;
    [SerializeField] private GameObject stages;

    [SerializeField] private List<GameObject> stageTxts = new List<GameObject>();

    [Header("Buttons")]
    [SerializeField] Button creditBtn;
    [SerializeField] Button creditExit;
    [SerializeField] Button settingBtn;
    [SerializeField] Button settingBGM;
    [SerializeField] Button settingSFX;
    [SerializeField] Button settingExit;
    [SerializeField] Sprite[] BGMSpr;
    [SerializeField] Sprite[] SFXSpr;
    bool bgmOn = true;
    bool sfxOn = true;

    [Header("Tabs")]
    [SerializeField] GameObject creditPanel;
    [SerializeField] GameObject settingPanel;

    public Image blackImage;
    public bool titleDebug = false;

    private void Awake()
    {
        BGMAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (!titleDebug)
        {
            EventManager.AddEvent("gotoCutScene", () => { cutSceneCoroutine = StartCoroutine(CutScene()); isCutScene = true; });
            EventManager.AddEvent("gotoTitle", () => TitleStart());
        }
        else
        {
            EventManager.AddEvent("gotoCutScene", () => TitleStart());
            EventManager.AddEvent("gotoTitle", () => TitleStart());
            cutSceneImg.gameObject.SetActive(false);
        }
        int bgmState = PlayerPrefs.GetInt("BGMState");
        int sfxState = PlayerPrefs.GetInt("SFXState");

        if (bgmState == 1) 
        {
            bgmOn = true;
        }
        else
        {
            bgmOn = false;
        }
        if (sfxState == 1) 
        {
            sfxOn = true;
        }
        else
        {
            sfxOn = false;
        }

        // 버튼 온클릭
        {
            creditBtn.onClick.AddListener(() =>
            {
                creditPanel.SetActive(true);
            });

            creditExit.onClick.AddListener(() =>
            {
                creditPanel.SetActive(false);
            });

            settingBtn.onClick.AddListener(() =>
            {
                settingPanel.SetActive(true);
            });

            settingBGM.onClick.AddListener(() =>
            {
                bgmOn = !bgmOn;

                if (bgmOn)
                {
                    settingBGM.GetComponent<Image>().sprite = BGMSpr[0];
                    BGMAudioSource.volume = 1;
                    PlayerPrefs.SetInt("BGMState", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    settingBGM.GetComponent<Image>().sprite = BGMSpr[1];
                    BGMAudioSource.volume = 0;
                    PlayerPrefs.SetInt("BGMState", 0);
                    PlayerPrefs.Save();
                }
            });

            settingSFX.onClick.AddListener(() =>
            {
                sfxOn = !sfxOn;

                if (sfxOn)
                {
                    settingSFX.GetComponent<Image>().sprite = SFXSpr[0];
                    SFXAudioSource.volume = 1;
                    PlayerPrefs.SetInt("SFXState", 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    settingSFX.GetComponent<Image>().sprite = SFXSpr[1];
                    SFXAudioSource.volume = 0;
                    PlayerPrefs.SetInt("SFXState", 0);
                    PlayerPrefs.Save();
                }
            });

            settingExit.onClick.AddListener(() =>
            {
                settingPanel.SetActive(false);
            });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCutScene)
        {
            StopCoroutine(cutSceneCoroutine);

            DOTween.To(() => BGMAudioSource.volume, value => BGMAudioSource.volume = value, 0, 1);
            blackImage.DOFade(1, 1).OnComplete(() =>
            {
                isCutScene = false;
                cutSceneImg.gameObject.SetActive(false);
                EventManager.Invoke("gotoTitle");
                blackImage.DOFade(0, 1);
            });
        }

        if (isTitle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickToStart();
            }
        }
    }

    IEnumerator CutScene()
    {
        blackImage.DOFade(1, 1);
        skipText.SetActive(true);
        yield return new WaitForSeconds(3);
        skipText.GetComponent<Text>().DOFade(0, 1);
        BGMAudioSource.clip = Audio_BGM_Legend;
        BGMAudioSource.Play();
        yield return new WaitForSeconds(4);
        cutSceneImg.GetComponent<Image>().color = Color.white;
        cutSceneImg.GetComponent<Image>().sprite = cutScene[0];
        blackImage.DOFade(0, 3);
        yield return new WaitForSeconds(10);
        blackImage.DOFade(1, 1);

        for (int i = 1; i < 5; i++)
        {
            yield return new WaitForSeconds(4);
            cutSceneImg.GetComponent<Image>().sprite = cutScene[i];
            blackImage.DOFade(0, 1);
            yield return new WaitForSeconds(10);
            blackImage.DOFade(1, 1);
        }

        yield return new WaitForSeconds(3);
        DOTween.To(() => BGMAudioSource.volume, value => BGMAudioSource.volume = value, 0, 2).OnComplete(() =>
        {
            isCutScene = false;
            cutSceneImg.gameObject.SetActive(false);
            EventManager.Invoke("gotoTitle");
            blackImage.DOFade(0, 1);
        });
    }

    void TitleStart()
    {
        isTitle = true;
        BGMAudioSource.clip = Audio_BGM_Title;
        BGMAudioSource.volume = 1;
        BGMAudioSource.Play();
        titlePanel.SetActive(true);
        StartCoroutine(TitleMove());

        startText.DOColor(new Color(0f, 0f, 0f, 1f), 1f).SetLoops(-1, LoopType.Yoyo);
        startText.GetComponent<Outline>().DOColor(new Color(1f, 1f, 1f, 1f), 1f).SetLoops(-1, LoopType.Yoyo);
    }

    IEnumerator TitleMove()
    {
        while(true)
        {
            if (!isTitle) yield break;

            yield return new WaitForSeconds(1.25f);
            titleImg.position = new Vector2(titleImg.position.x, titleImg.position.y + 0.1f);
            yield return new WaitForSeconds(1.25f);
            titleImg.position = new Vector2(titleImg.position.x, titleImg.position.y - 0.1f);
            yield return new WaitForSeconds(1.25f);
            titleImg.position = new Vector2(titleImg.position.x, titleImg.position.y - 0.1f);
            yield return new WaitForSeconds(1.25f);
            titleImg.position = new Vector2(titleImg.position.x, titleImg.position.y + 0.1f);
        }
    }

    public void ClickToStart() 
    {
        isTitle = false;
        startText.gameObject.SetActive(false);

        stages.transform.DOLocalMoveX(779, 1.5f);
        stageTxts[0].GetComponent<Button>().onClick.AddListener(() => { OnClickInGameBtn(); });
        stageTxts[0].transform.DOLocalMoveX(725f, 1.5f).SetEase(Ease.OutBounce).SetDelay(0.25f);
        stageTxts[1].transform.DOLocalMoveX(725f, 1.5f).SetEase(Ease.OutBounce).SetDelay(0.5f);
        stageTxts[2].transform.DOLocalMoveX(725f, 1.5f).SetEase(Ease.OutBounce).SetDelay(0.75f);
        stageTxts[3].transform.DOLocalMoveX(725f, 1.5f).SetEase(Ease.OutBounce).SetDelay(1f);
    }

    void OnClickInGameBtn() 
    {
        SceneController.LoadScene("InGame");
    }
}
