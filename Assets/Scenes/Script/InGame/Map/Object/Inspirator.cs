using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspirator : MonoBehaviour, ICollectible
{
    [SerializeField] AudioClip mClip;
    
    private Collider2D[] MapObjects;
    public void Collect()
    {
        MapObjects = Physics2D.OverlapAreaAll(
            GameManager.instance.Player.transform.position + Vector3.left * 25 + Vector3.up * 25,
            GameManager.instance.Player.transform.position + Vector3.right * 25 + Vector3.down * 25,
            LayerMask.GetMask("MapObject"));

        foreach(Collider2D obj in MapObjects)
        {
            if(obj.TryGetComponent(out ExperiencePickUp exp))
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, GameManager.instance.Player.transform.position, 10f * Time.deltaTime);
            }
        }
    }
}