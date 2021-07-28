using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MoveObj
{
    Animator animator;
    AudioSource audioSource;

    [SerializeField] private Transform damageRangeTrans = null;
    private Collider2D damageColl = null;

    private float firePower = 0f;

    [SerializeField] private float delay = 0.25f;
    private WaitForSeconds damageWait = null;

    private List<Enemy> rangedEnemy = new List<Enemy>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        damageColl = damageRangeTrans.GetComponent<Collider2D>();
        damageWait = new WaitForSeconds(delay);

        StartCoroutine(IncreaseFire());
        StartCoroutine(GiveDamage());
    }

    protected override void OnChangeProperty(Property prop)
    {
        if(prop.Equals(Property.FIRE))
        {
            SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Fire, 1);
            audioSource.Play();
        }
        else
        {
            SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_FireOff, 1);
            audioSource.Stop();
        }
    }

    private IEnumerator IncreaseFire()
    {
        while (true)
        {
            if (currentProperty.HasFlag(Property.FIRE))
            {
                firePower += Time.deltaTime * 1.5f;
            }
            else
            {
                firePower -= Time.deltaTime * 0.1f;
            }

            firePower = Mathf.Clamp(firePower, 0f, 3f);

            if (firePower > 0f)
            {
                if (firePower < 1)
                {
                    animator.Play("Torch_On");
                }
                else if (firePower < 2)
                {
                    animator.Play("Torch_On2");
                }
                else
                {
                    animator.Play("Torch_On3");
                }
            }
            else
            {
                animator.Play("Torch_Off");
            }

            yield return null;
        }
    }

    private IEnumerator GiveDamage()
    {
        int damage = 0;

        while (true)
        {
            if (firePower > 0f)
            {
                damage = Mathf.RoundToInt(Mathf.Clamp(firePower, 1f, 3f));

                for (int i = 0; i < rangedEnemy.Count; i++)
                {
                    rangedEnemy[i].GetDamage(damage);
                }

                yield return damageWait;
            }

            yield return null;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        rangedEnemy.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        rangedEnemy.Remove(enemy);
    }
}
