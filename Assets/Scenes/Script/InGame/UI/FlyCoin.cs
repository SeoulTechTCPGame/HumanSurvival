using UnityEngine;

public class FlyCoin : MonoBehaviour
{
    private float mLaunchSpeed = 1500f;
    private float mGravity = 2000f;
    private float mSpinSpeed = 360f;
    private Vector2 mVelocity;
    private RectTransform mRectTransform;

    private void Start()
    {
        mRectTransform = GetComponent<RectTransform>();
        mRectTransform.anchoredPosition += new Vector2(UnityEngine.Random.Range(-100, 100), 0);

        float angle = Random.Range(-45, 45);
        Vector2 launchDirection = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        mVelocity = mLaunchSpeed * launchDirection;
    }

    private void Update()
    {
        mVelocity.y -= mGravity * Time.unscaledDeltaTime;
        mRectTransform.anchoredPosition += mVelocity * Time.unscaledDeltaTime;

        mRectTransform.localRotation *= Quaternion.Euler(0, mSpinSpeed * Time.unscaledDeltaTime, 0);

        if (mRectTransform.anchoredPosition.y < -540) 
        {
            Destroy(gameObject);
        }
    }
}