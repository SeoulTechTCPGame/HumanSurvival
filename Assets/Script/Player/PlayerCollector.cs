using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
            collectible.Collect();
    }
}
