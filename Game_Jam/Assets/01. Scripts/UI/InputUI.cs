using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUI : MonoBehaviour
{
    [SerializeField] private OptionUIManager optionUIManager;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            optionUIManager.PauseBtnActiveSelf();
        }
    }
}
