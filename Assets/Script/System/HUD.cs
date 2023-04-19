using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  HUDUi: MonoBehaviour
{
    public enum InfoType { Exp, Level,Kill,Time,Health,Coin}
    public InfoType type;
    Text myText;
    Slider mySlider;
    private void Awake()
    {
        mySlider = GetComponent<Slider>();
        myText = GetComponent<Text>();
    }
    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.maxExp;
                
                mySlider.value = curExp / maxExp;
                //Debug.Log(mySlider.value);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Level:
                //only String can be Text. so use string.Format()
                //Text에는 문자열만 들어갈 수 있어서 format 해준다. 
                //string.Format({index:데이터 포맷},데이터)
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoType.Time:
                int min = Mathf.FloorToInt(GameManager.instance.gameTime / 60);
                int sec=Mathf.FloorToInt(GameManager.instance.gameTime % 60);
                //D1,D2는 자리수를 고정하는 포맷
                myText.text = string.Format("{0:D2}:{1:D2}", min,sec);
                break;
            case InfoType.Coin:
                myText.text = string.Format("{0:F0}", GameManager.instance.coin);
                break;
        }
    }
}

