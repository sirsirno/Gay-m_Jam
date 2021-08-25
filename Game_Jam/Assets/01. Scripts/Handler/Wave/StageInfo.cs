using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StageInfo", menuName = "StageInfo", order = 2)]
public class StageInfo : ScriptableObject
{
    public List<WaveInfo> waveInfos = new List<WaveInfo>();
}
