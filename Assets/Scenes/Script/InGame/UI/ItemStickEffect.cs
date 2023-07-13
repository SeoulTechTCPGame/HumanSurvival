using UnityEngine;

public class ItemStickEffect : MonoBehaviour
{
    private float mMinScaleY = 0.5f;
    private float mMaxScaleY = 1.5f;
    private float mRotationSpeed = 45f;
    private float mScaleSpeed = 1f;
    private float mOriginalY;
    private float mOriginalX;

    private void Start()
    {
        transform.Rotate(0f, 0f, UnityEngine.Random.Range(-20f, 20f));
        mOriginalY = transform.localPosition.y;
        mOriginalX = transform.localPosition.x;
    }
    private void Update()
    {
        ChangeScaleAndRotate();
    }
    private void ChangeScaleAndRotate()
    {
        float sinValue = Mathf.Sin(Time.fixedDeltaTime * mScaleSpeed);
        float scale = Mathf.Lerp(mMinScaleY, mMaxScaleY, (sinValue + 1f) / 2f);
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        transform.localPosition = new Vector3(mOriginalX + scale / 2f, mOriginalY + scale / 2f, transform.localPosition.z);
        transform.Rotate(0f, 0f, mRotationSpeed * Time.fixedDeltaTime);
    }
}
