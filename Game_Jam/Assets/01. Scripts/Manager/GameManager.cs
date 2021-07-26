using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTon
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    Debug.LogError("게임메니져가 존재하지 않습니다!");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)                       // 만약 instance가 비어있다면
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)                   // 비어있진 않은데 instance가 자신이 아니라면
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public List<PlayerProperty> playerList = new List<PlayerProperty>();
    public int chainCount = 0;
    public int stage = 1;
    public int remainEnemyCount = 0;
    public int maxHp;
    public int currentHp;
}
