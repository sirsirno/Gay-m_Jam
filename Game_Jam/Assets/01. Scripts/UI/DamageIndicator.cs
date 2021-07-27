using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageIndicator : MonoBehaviour
{
    public GameObject damageText;


    void Start()
    {
        PoolManager.CreatePool<DamageDisplay>(damageText, transform, 10);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            int random = Random.Range(10, 100);
            DamageDisplay(random, this.gameObject);
        }
    }

    public static void DamageDisplay(int damage, GameObject hitObj)
    {
        DamageDisplay damageObj = PoolManager.GetItem<DamageDisplay>();
        damageObj.GetComponent<Text>().text = damage.ToString();
        damageObj.transform.position = hitObj.transform.position;

        if(damage <= GameManager.Instance.lowDamageLimit)
        {
            damageObj.GetComponent<Text>().color = GameManager.Instance.lowDamageColor;
        }
        else if (damage <= GameManager.Instance.midDamageLimit)
        {
            damageObj.GetComponent<Text>().color = GameManager.Instance.midDamageColor;
        }
        else
        {
            damageObj.GetComponent<Text>().color = GameManager.Instance.highDamageColor;
        }

        float randomX = Random.Range(-2, 2);

        damageObj.transform.DOMoveX(randomX, 0.75f).SetRelative();
        damageObj.transform.DOMoveY(3, 0.75f).SetRelative();
        damageObj.GetComponent<Text>().DOFade(0, 0.75f);
    }
}
