using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private List<Sprite> sourceImgs = new List<Sprite>();
    [SerializeField]
    private GameObject loadingEnemy;
    [SerializeField]
    private Button clickToStartBtn;

    private static string nextScene;
    private float a = 0;
    public Image image;

    public static bool isLoaded = false;
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
        int idx = Random.Range(0, 2);
        progressBar.GetComponent<Image>().sprite = sourceImgs[idx];
    }
    private void Update()
    {
        if (loadingEnemy.transform.position.x > 5&& !isLoaded) 
        {
            loadingEnemy.transform.position = new Vector3(-5f, -2f, 0);
        }
        else if(!isLoaded)
        {
            loadingEnemy.transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime* 5f;
        }
    }


    public static void LoadScene(string sceneName)
    {
        
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }


    IEnumerator LoadSceneProgress()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        isLoaded = false;
        if (async == null) yield break;
        async.allowSceneActivation = false;

        float timer = 0f;
        while (!async.isDone)
        {
            yield return null;

            if (async.progress < 0.9f)
            {
                yield return null;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    isLoaded = true;
                    clickToStartBtn.gameObject.SetActive(true);
                    loadingEnemy.SetActive(false);
                    clickToStartBtn.onClick.AddListener(() => {OnClickContinueBtn(); });
                    Time.timeScale = 0;
                    StartCoroutine(FadeIn());
                    yield return new WaitForSeconds(1f);
                    async.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
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
    void OnClickContinueBtn() 
    {
        Time.timeScale = 1f;
        clickToStartBtn.gameObject.SetActive(false);
    }
}
