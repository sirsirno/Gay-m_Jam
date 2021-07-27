using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSample : MonoBehaviour
{
    void Start()
    {
        EventManager.AddEvent("FireBomb", () => { print("Äâ±¤!!!!!!!!!! °¡º¸ÀÚ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); });
    }
}
