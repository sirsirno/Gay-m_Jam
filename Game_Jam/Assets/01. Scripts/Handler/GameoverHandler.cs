using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverHandler : MonoBehaviour
{
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

    }
}
