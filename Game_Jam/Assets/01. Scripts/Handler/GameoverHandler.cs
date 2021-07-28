using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameoverHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject fadeUI;
    [SerializeField] GameObject jedan_gameOver;
    [SerializeField] GameObject jedan_gameOver_animtion;
    [SerializeField] GameObject spotlight;
    [SerializeField] GameObject players;

    public CanvasGroup bgPanel;
    public GameObject gameOverPanel;
    public Image chapSsal;

    [Header("¹öÆ°µé")]
    public Button gotoMainBtn;
    public Button retryBtn;

    private void Start()
    {
        bgPanel.alpha = 0;
        gameOverPanel.transform.localScale = Vector3.zero;

        bgPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);

        gotoMainBtn.onClick.AddListener(() => {
            GameManager.Instance.ResetValue();
            PoolManager.ResetPool();
            SceneController.LoadScene("Title");
        });

        retryBtn.onClick.AddListener(() => {
            GameManager.Instance.ResetValue();
            PoolManager.ResetPool();
            SceneController.LoadScene("InGame");
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            collision.gameObject.SetActive(false);
            GameManager.Instance.enemyList.Remove(enemy);
            GameManager.Instance.cameraHandler.CameraImpulse(1f);
            StoneFragDeadEffectHandler.CreateStoneFrag(transform.position);
            GameManager.Instance.uiManager.SetLeftNumber(GameManager.Instance.uiManager.currentLeft - 1);

            GameManager.Instance.currentHp -= enemy.hpDecreaseAmount;
            GameManager.Instance.uiManager.SetHpFill();

            if (GameManager.Instance.currentHp < 1)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        GameManager.Instance.cameraHandler.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameManager.Instance.transform;
        fadeUI.SetActive(true);
        jedan_gameOver.SetActive(true);
        StartCoroutine(GameOverTimeLine());
    }

    IEnumerator GameOverTimeLine()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        jedan_gameOver.SetActive(false);
        jedan_gameOver_animtion.SetActive(true);
        spotlight.SetActive(true);
        players.SetActive(true);

        yield return new WaitForSecondsRealtime(2.5f);

        gameOverCanvas.SetActive(true);

        bgPanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_GameOver, 1);

        bgPanel.DOFade(1f, 1f).SetUpdate(true);
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);
        chapSsal.gameObject.SetActive(true);
        chapSsal.rectTransform.DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.OutBounce).SetUpdate(true);
    }
}
