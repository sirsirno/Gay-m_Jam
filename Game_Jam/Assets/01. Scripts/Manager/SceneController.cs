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

    private static string nextScene;

    public static bool isLoaded = false;
    void Start()
    {
        StartCoroutine(LoadSceneProgress());
        int idx = Random.Range(0, 2);
        progressBar.GetComponent<Image>().sprite = sourceImgs[idx];
    }
    private void Update()
    {
        if (loadingEnemy.transform.position.x > 5) 
        {
            loadingEnemy.transform.position = new Vector3(-5f, 0, 0);
        }
        else
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
                    async.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }
}
