using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject DefaultPanel; //OptionBackground
    public GameObject FirstPanel;   //OptionMenu_1Page
    public GameObject SecondPanel;  //OptionMenu_2Page
    public GameObject ThirdPanel;   //DataRecovery
    public Text buttontext; //DataRecovery텍스트
    //Panel초기화
    private void Awake()
    {
        DefaultPanel.SetActive(true);
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
    }

    //뒤로가기 버튼
    public void ClickBackButton()
    {
        //TODO: 이 상태에서 back 버튼을 누르면 메인화면으로 가야한다.
        /*
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false) { }
        */
        //두번째 페이지에서 첫번째 페이지로 이동
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == true & ThirdPanel.activeSelf == false)
        {
            SecondPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //DataRecovery에서 첫번째 페이지로 이동
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
            buttontext.GetComponent<Text>().text = "data recovery";
        }
        //그외는 button의 onClick에서 사용
    }
}
