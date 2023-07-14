using UnityEngine;
using UnityEngine.UI;

public class FlyItem : MonoBehaviour
{
    public Vector3 EndPoint;
    private float mSpeed = 1000f;

    private void Update()
    {
        float step = mSpeed * Time.unscaledDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, EndPoint, step);

        if (transform.position == EndPoint)
        {
            Destroy(gameObject);
        }
    }
    public void SetImage(Sprite img)
    {
        GetComponent<Image>().sprite = img;
    }
}