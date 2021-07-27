using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPassive : MonoBehaviour
{
    private bool isWaterOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.GetComponent<PlayerProperty>() != null)
            {
                if (collision.GetComponent<PlayerProperty>().MyProperty == Property.WATER)
                {
                    isWaterOn = true;
                    transform.GetChild(0).gameObject.SetActive(true);
                    SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Water, 1);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isWaterOn)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.GetComponent<Enemy>().slowPassiveScale != 0.65f)
            {
                collision.GetComponent<Enemy>().slowPassiveScale = 0.65f;
                collision.GetComponent<Move_GoRight>().SetValue(collision.GetComponent<Move_GoRight>().GetValue() * 0.65f);
            }
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.GetComponent<Enemy>().slowPassiveScale != 1f)
            {
                collision.GetComponent<Enemy>().slowPassiveScale = 1f;
                collision.GetComponent<Move_GoRight>().SetValue(collision.GetComponent<Move_GoRight>().GetValue() / 0.65f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.GetComponent<PlayerProperty>() != null)
            {
                if (collision.GetComponent<PlayerProperty>().MyProperty == Property.WATER)
                {
                    isWaterOn = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
}
