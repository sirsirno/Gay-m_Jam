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
    public Property currentProperty = Property.FIRE;

    public List<Enemy> enemyList = new List<Enemy>();

    [HideInInspector] public CameraHandler cameraHandler;
    public Transform projectilesTrans = null;

    public UIManager uiManager;

    public int chainCount = 0;
    public int stage = 1;
    public int remainEnemyCount = 0;
    public int maxHp;
    public int currentHp;

    [Header("데미지 인디케이터")]
    public Color lowDamageColor;
    public Color midDamageColor;
    public Color highDamageColor;

    public int lowDamageLimit;
    public int midDamageLimit;

    [Header("스킬 오브젝트")]
    [System.NonSerialized] public Skill[] skills = new Skill[4];
    [HideInInspector] public FireDefault QDefault;
}
