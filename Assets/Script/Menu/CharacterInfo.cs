using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterInfo : MonoBehaviour, IPointerEnterHandler
{
    public int character;   //캐릭터 고유 번호
    public Image infoImage; //설명란 이미지
    public Image buttonIamge;   //버튼 이미지
    public TMP_Text characterName;  //캐릭터 이름
    public TMP_Text characterExplain;   //캐릭터 설명

    string Name;    //캐릭터 이름 글
    string explain; //캐릭터 설명 글

    void Start()
    {
        switch (character)
        {
            case 1:
                this.Name = "캐릭터 이름1";
                this.explain = "캐릭터 설명1";
                break;

            case 2:
                this.Name = "캐릭터 이름2";
                this.explain = "캐릭터 설명2";
                break;

            case 3:

                this.Name = "캐릭터 이름3";
                this.explain = "캐릭터 설명3";
                break;

            case 4:
                this.Name = "캐릭터 이름4";
                this.explain = "캐릭터 설명4";
                break;

            case 5:
                this.Name = "캐릭터 이름5";
                this.explain = "캐릭터 설명5";
                break;

            case 6:
                this.Name = "캐릭터 이름6";
                this.explain = "캐릭터 설명6";
                break;

            case 7:
                this.Name = "캐릭터 이름7";
                this.explain = "캐릭터 설명7";
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        characterName.text = this.Name;
        characterExplain.text = this.explain;
        infoImage.GetComponent<Image>().sprite = buttonIamge.GetComponent<Image>().sprite;
    }

}

