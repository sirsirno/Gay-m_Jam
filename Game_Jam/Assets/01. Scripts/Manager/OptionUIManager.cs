using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum OptionBtnIdx
{
    EXIT,
    PAUSE,
    SETTING,
    SETTINGEXIT,
    GAMEOVER,
    RESTART,
    SOUND,
    CREDIT,
    EXITPANEL
};

public enum SoundSetting
{
    BGM,
    SFX
};

public class OptionUIManager : MonoBehaviour
{
    private Sequence optionSequence;

    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private CanvasGroup pausePopup;
    [SerializeField] private CanvasGroup settingPanel;
    [SerializeField] private CanvasGroup defaultPanel;
    [SerializeField] private CanvasGroup soundPanel;
    [SerializeField] private CanvasGroup creditPanel;
    [SerializeField] private CanvasGroup gameOverPanel;

    [Header("사운드 관련")]
    [SerializeField] private Image[] soundBtnImage;
    [SerializeField] private Sprite[] BGM;
    [SerializeField] private Sprite[] SFX;

    [Header("버튼 관련")]
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button bgmBtn;
    [SerializeField] private Button sfxBtn;
    [SerializeField] private Button creditBtn;
    [SerializeField] private Button restartBtn;
    


    [NonSerialized] public bool BGMSound = true;
    [NonSerialized] public bool SFXSound = true;



    void Start()
    {
/*        restartBtn.onClick.AddListener(() =>
        {
            OnBtn(OptionBtnIdx.RESTART); 
        });*/

        resumeBtn.onClick.AddListener(() =>
        {
            OnBtn(OptionBtnIdx.EXIT);
        });

        settingBtn.onClick.AddListener(() =>
        {
            OnBtn(OptionBtnIdx.SETTING);
        });

        soundBtn.onClick.AddListener(() =>
        {
            OnBtn(OptionBtnIdx.SOUND);
        });

        bgmBtn.onClick.AddListener(() =>
        {
            OnSoundBtn(SoundSetting.BGM);
        });

        sfxBtn.onClick.AddListener(() =>
        {
            OnSoundBtn(SoundSetting.SFX);
        });

        creditBtn.onClick.AddListener(() =>
        {
            OnBtn(OptionBtnIdx.CREDIT);
        });

        optionSequence = DOTween.Sequence().SetAutoKill(false);
    }

    void OpenUI(CanvasGroup cvsGroup, bool isOpen,  float time)
    {
        cvsGroup.gameObject.SetActive(isOpen);
        cvsGroup.interactable = isOpen;
        cvsGroup.blocksRaycasts = isOpen;
        optionSequence.Append(DOTween.To(() => cvsGroup.alpha, x => cvsGroup.alpha = x, isOpen ? 1 : 0, time).SetUpdate(true));
    }

    public void PauseBtnActiveSelf() 
    {
        
        if (!pausePanel.gameObject.activeSelf)
        {
            OnBtn(OptionBtnIdx.PAUSE);
        }
        else if (pausePanel.gameObject.activeSelf) 
        {
            OnBtn(OptionBtnIdx.EXIT);
        }
    }
  
    public void OnBtn(OptionBtnIdx btnIdx, CanvasGroup exitCvsGroup = null, CanvasGroup openCvsGroup = null)
    {
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_UI, 1);
        switch (btnIdx)
        {
            case OptionBtnIdx.EXIT:
                OpenUI(pausePanel, false, 0.3f);
                break;
            case OptionBtnIdx.PAUSE:
                OpenUI(pausePanel, true, 0.3f);
                break;
            case OptionBtnIdx.SETTING:
                OpenUI(pausePopup, false, 0.3f);
                OpenUI(settingPanel, true, 0.3f);
                break;
            case OptionBtnIdx.GAMEOVER:
                OpenUI(gameOverPanel, true, 0.3f);
                break;
            case OptionBtnIdx.RESTART:
                OpenUI(gameOverPanel, false, 0.3f);
                break;
            case OptionBtnIdx.SOUND:
                OpenUI(defaultPanel, false, 0.3f);
                OpenUI(soundPanel, true, 0.3f);
                break;
            case OptionBtnIdx.CREDIT:
                OpenUI(defaultPanel, false, 0.3f);
                OpenUI(creditPanel, true, 0.3f);
                break;
            case OptionBtnIdx.EXITPANEL:
                OpenUI(exitCvsGroup, false, 0.3f);
                OpenUI(openCvsGroup, true, 0.3f);
                break;
        }
    }
    public void OnSoundBtn(SoundSetting sound)
    {
        switch (sound)
        {
            case SoundSetting.BGM:
                if (BGMSound)
                {
                    soundBtnImage[0].sprite = BGM[1];
                }
                else
                {
                    soundBtnImage[0].sprite = BGM[0];
                }
                BGMSound = !BGMSound;
                break;
            case SoundSetting.SFX:
                if (SFXSound)
                {
                    soundBtnImage[1].sprite = SFX[1];
                }
                else
                {
                    soundBtnImage[1].sprite = SFX[0];
                }
                SFXSound = !SFXSound;
                break;
        }
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}