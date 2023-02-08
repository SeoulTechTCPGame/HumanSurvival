using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUPSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponent<Character>();
        if (c != null)
        {
            //GetComponent<IPickUpObject>().OnPickUp(c);
            // 태그에 따라 힐인지 경험치인지
            if (collision.gameObject.tag == "ExpObj")
            {
                Debug.Log("pick exp");
                // c.GetExp();
            }
            if (collision.gameObject.tag == "HealObj")
            {
                Debug.Log("pick health");
                //c.Heal();
            }
            Destroy(gameObject);
            if (collision.gameObject.tag == "Coins")
            {
                Debug.Log("pick coin");
            }

        }
    }
}
