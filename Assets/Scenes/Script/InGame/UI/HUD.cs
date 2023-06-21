using UnityEngine;
using UnityEngine.UI;

public class HUD: MonoBehaviour
{
    public enum EInfoType { Exp, Level,Kill,Time,Health,Coin}
    public EInfoType Type;
    private Text mMyText;
    private Slider mMySlider;

    private void Awake()
    {
        mMySlider = GetComponent<Slider>();
        mMyText = GetComponent<Text>();
    }
    private void LateUpdate()
    {
        switch (Type)
        {
            case EInfoType.Exp:
                float curExp = GameManager.instance.Exp;
                float maxExp = GameManager.instance.MaxExp;
                
                mMySlider.value = curExp / maxExp;
                //Debug.Log(mySlider.value);
                break;
            case EInfoType.Kill:
                mMyText.text = string.Format("{0:F0}", GameManager.instance.Kill);
                break;
            case EInfoType.Level:
                //only String can be Text. so use string.Format()
                //Text에는 문자열만 들어갈 수 있어서 format 해준다. 
                //string.Format({index:데이터 포맷},데이터)
                mMyText.text = string.Format("Lv.{0:F0}",GameManager.instance.Level);
                break;
            case EInfoType.Time:
                int min = Mathf.FloorToInt(GameManager.instance.GameTime / 60);
                int sec=Mathf.FloorToInt(GameManager.instance.GameTime % 60);
                //D1,D2는 자리수를 고정하는 포맷
                mMyText.text = string.Format("{0:D2}:{1:D2}", min,sec);
                break;
            case EInfoType.Coin:
                mMyText.text = string.Format("{0:F0}", GameManager.instance.Coin);
                break;
        }
    }
}