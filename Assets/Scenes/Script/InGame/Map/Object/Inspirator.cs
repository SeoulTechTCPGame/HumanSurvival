using UnityEngine;

public class Inspirator : MonoBehaviour, ICollectible
{
    [SerializeField] AudioClip mClip;

    private float mTimer = 0;

    public void Collect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        mTimer += Time.deltaTime;
        SoundManager.instance.PlaySoundEffect(mClip);

        GameObject[] MapObjects = GameObject.FindGameObjectsWithTag("CollectibleObj");
        if (MapObjects.Length == 0) { return; }
        
        foreach(GameObject obj in MapObjects)
        {
            if(obj.TryGetComponent(out ExperiencePickUp exp))
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, GameManager.instance.Player.transform.position, 15f * Time.deltaTime);
            }
        }

        if (mTimer > 5f)
        {
            gameObject.SetActive(false);
        }
    }
}