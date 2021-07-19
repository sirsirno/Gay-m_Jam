using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        EventManager.AddEvent("gotoTitle", () => TitleStart());
    }

    void TitleStart()
    {
        Debug.Log("여기서부턴 타이틀 애니");
    }

}
