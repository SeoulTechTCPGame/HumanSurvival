using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 mOffset;
    private void Start()
    {
        mOffset = transform.position - Player.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = Player.transform.position + mOffset;
    }
}