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
                    Debug.LogError("���Ӹ޴����� �������� �ʽ��ϴ�!");
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)                       // ���� instance�� ����ִٸ�
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)                   // ������� ������ instance�� �ڽ��� �ƴ϶��
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

    [Header("������ �ε�������")]
    public Color lowDamageColor;
    public Color midDamageColor;
    public Color highDamageColor;

    public int lowDamageLimit;
    public int midDamageLimit;

    [Header("��ų ������Ʈ")]
    [System.NonSerialized] public Skill[] skills = new Skill[4];
    [HideInInspector] public FireDefault QDefault;
}
