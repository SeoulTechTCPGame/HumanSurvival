using UnityEngine;
using UnityEngine.UI;

public class FlyItem : MonoBehaviour
{
    public Vector2 EndPoint;
    public Vector2 StartPoint;
    private RectTransform mRectTransform;
    private float mSpeed = 1500f;
    private float mStartTime;

    void Start()
    {
        mRectTransform = GetComponent<RectTransform>();
        mStartTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float distCovered = (Time.realtimeSinceStartup - mStartTime) * mSpeed;
        float fractionOfJourney = distCovered / Vector2.Distance(StartPoint, EndPoint);
        mRectTransform.anchoredPosition = Vector2.Lerp(StartPoint, EndPoint, fractionOfJourney);

        if (mRectTransform.anchoredPosition == EndPoint)
        {
            Destroy(mRectTransform.gameObject);
        }
    }
    public void SetImage(Sprite img)
    {
        GetComponent<Image>().sprite = img;
    }
}