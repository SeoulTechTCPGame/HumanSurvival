using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    public int stage;   //캐릭터 고유 번호
    public TMP_Text stageName;  //스테이지 이름
    public TMP_Text stageTime;   //스테이지 플레이 시간
    public TMP_Text stageDoubleSpeed;   //스테이지 배속
    public TMP_Text stageGoldCoinBonus;   //스테이지 골드 보너스
    public TMP_Text stageLuckBonus;   //스테이지 행운 보너스
    public TMP_Text stageExperienceBonus;   //스테이지 경험치 보너스

    string Name;    //스테이지 이름 글
    string time;    //플레이 시간
    int doubleSpeed;    //시간 배속
    int goldCoinBonus;  //보너스 골드
    int luckBonus;  //보너스 행운
    int experienceBonus;    //보너스 경험치

    void Start()
    {
        switch (stage)
        {
            case 1:
                this.Name = "Stage 1";
                this.time = "30:00";
                this.doubleSpeed = 1;
                this.goldCoinBonus = 1;
                this.luckBonus = 1;
                this.experienceBonus = 1;
                break;

            case 2:
                this.Name = "Stage 2";
                this.time = "30:00";
                this.doubleSpeed = 1;
                this.goldCoinBonus = 1;
                this.luckBonus = 1;
                this.experienceBonus = 1;
                break;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        stageName.text = this.Name;
        stageTime.text = "Play time" + this.time;
        stageDoubleSpeed.text = "Gold Coin Bonus" + this.doubleSpeed.ToString();
        stageGoldCoinBonus.text = "Gold Coin Bonus" + this.goldCoinBonus.ToString();
        stageLuckBonus.text = "Luck Bonus" + this.luckBonus.ToString();
        stageExperienceBonus.text = " Experience Bonus" + this.experienceBonus.ToString();
    }
}
