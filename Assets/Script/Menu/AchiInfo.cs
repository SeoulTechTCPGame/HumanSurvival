using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using TMPro;

public class AchiInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mAchi;
    [SerializeField] Toggle mAchiToggle;
    [SerializeField] Image mAchiImage;
    [SerializeField] Image mThisAchiIamge;
    [SerializeField] Image mAchiImageBG;
    [SerializeField] TMP_Text mThisAchiName;
    [SerializeField] TMP_Text mAchiName;
    [SerializeField] TMP_Text mAchiExplain;
    [SerializeField] TMP_Text mAchiObtain;

    string mExplain;
    string mObtain;

    // Start is called before the first frame update
    void Start(){
        switch(mAchi){
            case 1:
                this.mExplain = "레벨 5에 도달";
                this.mObtain = "날개";
                break;
            
            case 2:
                this.mExplain = "레벨 10에 도달";
                this.mObtain = "왕관";
                break;

            case 3:
                this.mExplain = "광기의 숲에서 레벨 20에 도달";
                this.mObtain = "화려한 도서관";
                break;

            case 4:
                this.mExplain = "아무 캐릭터로 1분 생존";
                this.mObtain = "검은 심장";
                break;

            case 5:
                this.mExplain = "젠나로로 5분 생존";
                this.mObtain = "붉은 심장";
                break;

            case 6:
                this.mExplain = "아무 캐릭터로 10분 생존";
                this.mObtain = "하얀 비둘기";
                break;

            case 7:
                this.mExplain = "성경 레벨 4까지 올리기";
                this.mObtain = "팔 보호대";
                break;

            case 8:
                this.mExplain = "성수 레벨 4까지 올리기";
                this.mObtain = "촛대";
                break;

            case 9:
                this.mExplain = "불의 지팡이 레벨 4까지 올리기";
                this.mObtain = "아르카";
                break;

            case 10:
                this.mExplain = "번개 반지 레벨 4까지 올리기";
                this.mObtain = "포르타";
                break;

            case 11:
                this.mExplain = "마법 지팡이 레벨 7까지 올리기";
                this.mObtain = "복제 반지";
                break;

            case 12:
                this.mExplain = "하얀 비둘기 레벨 7까지 올리기";
                this.mObtain = "검은 비둘기";
                break;

            case 13:
                this.mExplain = "마늘을 레벨 7까지 올리기";
                this.mObtain = "포";
                break;

            case 14:
                this.mExplain = "토로나의 상자 레벨 9 달성";
                this.mObtain = "전능";
                break;

            case 15:
                this.mExplain = "동시에 무기 6가지 진화";
                this.mObtain = "토로나의 상자";
                break;

            case 16:
                this.mExplain = "6개의 무기를 소지";
                this.mObtain = "빈 책";
                break;

            case 17:
                this.mExplain = "누적 1,000의 체력을 회복";
                this.mObtain = "시스터 클레리씨";
                break;

            case 18:
                this.mExplain = "빛 물체 20개 파괴";
                this.mObtain = "불의 지팡이";
                break;

            case 19:
                this.mExplain = "치킨 5개 발견";
                this.mObtain = "마늘";
                break;

            case 20:
                this.mExplain = "작은 클로버 발견";
                this.mObtain = "클로버";
                break;

            case 21:
                this.mExplain = "흡입기 발견";
                this.mObtain = "매혹구";
                break;

            case 22:
                this.mExplain = "회중시계 발견";
                this.mObtain = "시곗바늘";
                break;

            case 23:
                this.mExplain = "묵주 발견";
                this.mObtain = "십자가";
                break;

            case 24:
                this.mExplain = "돌가면 발견";
                this.mObtain = "돌가면";
                break;

            case 25:
                this.mExplain = "그림 그리모어 발견";
                this.mObtain = "진화 목록";
                break;

            case 26:
                this.mExplain = "매직 뱅어 발견";
                this.mObtain = "음악 선택";
                break;

            case 27:
                this.mExplain = "은하수 지도 발견";
                this.mObtain = "일시정지 메뉴 지도";
                break;

            case 28:
                this.mExplain = "마녀의 눈물 발견";
                this.mObtain = "단축 모드";
                break;

            case 29:
                this.mExplain = "누적 3,000마리 적을 처치";
                this.mObtain = "번개 반지";
                break;

            case 30:
                this.mExplain = "컬렉션에 50개의 항목을 채우기";
                this.mObtain = "캐릭터 커스터마이징";
                break;

            case 31:
                this.mExplain = "컬렉션에 60개의 항목을 채우기";
                this.mObtain = "지우기";
                break;

            case 32:
                this.mExplain = "채찍 진화";
                this.mObtain = "골드 500개";
                break;

            case 33:
                this.mExplain = "마법 지팡이 진화";
                this.mObtain = "골드 500개";
                break;

            case 34:
                this.mExplain = "단검 진화";
                this.mObtain = "골드 500개";
                break;

            case 35:
                this.mExplain = "성수 진화";
                this.mObtain = "골드 500개";
                break;

            case 36:
                this.mExplain = "번개 반지 진화 진화";
                this.mObtain = "골드 500개";
                break;

            case 37:
                this.mExplain = "성경 진화";
                this.mObtain = "골드 500개";
                break;

            case 38:
                this.mExplain = "불의 지팡이 진화";
                this.mObtain = "골드 500개";
                break;
                
            case 39:
                this.mExplain = "마늘 진화";
                this.mObtain = "골드 500개";
                break;
        }

        if (UserInfo.instance.UserDataSet.Achievements[mAchi]) {
            mAchiToggle.GetComponent<Toggle>().isOn = true;
        }
        mThisAchiName.text = this.mExplain;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (UserInfo.instance.UserDataSet.Achievements[mAchi]) {
            mAchiName.text = "획득";
            mAchiImageBG.color = new Color(0f, 1f, 1f, 1f);
        }
        else{
            mAchiName.text = "";
            mAchiImageBG.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        
        mAchiExplain.text = this.mExplain;
        mAchiObtain.text = this.mObtain;
        mAchiImage.GetComponent<Image>().sprite = mThisAchiIamge.GetComponent<Image>().sprite;
    }

}
