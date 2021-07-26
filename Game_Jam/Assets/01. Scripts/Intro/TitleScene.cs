using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] OptionUIManager optionUIManager;
    [SerializeField] private Button startButton;
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
        Debug.Log("���⼭���� Ÿ��Ʋ �ִ�");
        isTitle = true;
        startButton.gameObject.SetActive(true);
        startButton.onClick.AddListener(() => { ClickToStart(); });

        audioSource.Play();
    }

    void ClickToStart() 
    {
        SceneController.LoadScene("InGame");
        isTitle = false;
    }
}
