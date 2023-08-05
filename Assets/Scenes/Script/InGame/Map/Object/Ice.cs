using UnityEngine;
using UnityEngine.UI;

public class Ice : MonoBehaviour, ICollectible
{
    [SerializeField] AudioClip mClip;

    private float mTime;
    private float mBreakTime = 5.0f;
    private bool mbOnBreak = false;

    private void Start()
    {
        mTime = 0;
    }

    public void Collect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        SoundManager.instance.PlaySoundEffect(mClip);
        if (mbOnBreak)
        {
            mTime = 0;
            return;
        }
        mTime += Time.deltaTime;
        EnemyFrozen();

        if (mTime >= mBreakTime)
        {
            BreakFrozen();
            gameObject.SetActive(false);
        }
    }
    private void EnemyFrozen()
    {
        GameManager.instance.EnemyTimeScale = 0;
        GameManager.instance.Character.BDamageImmune = true;
        GameManager.instance.BlueFilter.SetActive(true);
    }
    private void BreakFrozen()
    {
        GameManager.instance.EnemyTimeScale = 1;
        GameManager.instance.Character.BDamageImmune = false;
        GameManager.instance.BlueFilter.SetActive(false);
    }
}