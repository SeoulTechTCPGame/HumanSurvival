using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour, ICollectible
{
    [SerializeField] AudioClip mClip;
    [SerializeField] GameObject mIceFilter;

    private Coroutine mBreakFrozenCoroutine;

    private void Start()
    {
        mIceFilter = GameObject.Find("IceFilter");
    }

    public void Collect()
    {
        SoundManager.instance.PlaySoundEffect(mClip);
        EnemyFrozen();

        if(mBreakFrozenCoroutine != null)
        {
            StopCoroutine(mBreakFrozenCoroutine);
        }
        mBreakFrozenCoroutine = StartCoroutine(BreakFrozenAfterDelay(5.0f));
        gameObject.SetActive(false);
    }
    private void EnemyFrozen()
    {
        GameManager.instance.EnemyTimeScale = 0;
        GameManager.instance.Character.BDamageImmune = true;
        mIceFilter.SetActive(true);
    }
    private void BreakFrozen()
    {
        GameManager.instance.EnemyTimeScale = 1;
        GameManager.instance.Character.BDamageImmune = false;
        mIceFilter.SetActive(false);
    }
    private IEnumerator BreakFrozenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BreakFrozen();
    }
}