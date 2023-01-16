using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using TMPro;

public class AchiInfo : MonoBehaviour, IPointerEnterHandler
{
    public int achi;
    public Toggle achiToggle;
    public Image achiImage;
    public Image thisAchiIamge;
    public Image achiImageBG;
    public TMP_Text thisAchiName;
    public TMP_Text achiName;
    public TMP_Text achiExplain;
    public TMP_Text achiObtain;

    string explain;
    string obtain;

    // Start is called before the first frame update
    void Start(){
        switch(achi){
            case 1:
                this.explain = "레벨 5에 도달";
                this.obtain = "날개";
                break;
            
            case 2:
                this.explain = "레벨 10에 도달";
                this.obtain = "왕관";
                break;

            case 3:
                this.explain = "광기의 숲에서 레벨 20에 도달";
                this.obtain = "화려한 도서관";
                break;

            case 4:
                this.explain = "화려한 도서관에서 레벨 40에 도달";
                this.obtain = "유제품 공장";
                break;

            case 5:
                this.explain = "유제품 공장에서 레벨 60에 도달";
                this.obtain = "갈로의 탑";
                break;

            case 6:
                this.explain = "갈로의 탑에서 레벨 80에 도달합니다";
                this.obtain = "마그나 예배당";
                break;

            case 7:
                this.explain = "반전 갈로의 탑에서 레벨 80에 도달합니다";
                this.obtain = "하이퍼 작은 다리";
                break;

            case 8:
                this.explain = "아무 캐릭터로 1분 생존";
                this.obtain = "검은 심장";
                break;

            case 9:
                this.explain = "파스콸리나로 5분 생존";
                this.obtain = "룬 트레이서";
                break;

            case 10:
                this.explain = "젠나로로 5분 생존";
                this.obtain = "붉은 심장";
                break;

            case 11:
                this.explain = "아무 캐릭터로 10분 생존";
                this.obtain = "하얀 비둘기";
                break;

            case 12:
                this.explain = "푸날라로 10분 생존";
                this.obtain = "피에라 데 투펠로";
                break;

            case 13:
                this.explain = "푸날라로 15분 생존";
                this.obtain = "에잇 더 스패로우";
                break;

            case 14:
                this.explain = "지오반나로 15분 생존";
                this.obtain = "마녀의 고양이";
                break;

            case 15:
                this.explain = "포페아로 15분 생존";
                this.obtain = "마나의 노래";
                break;

            case 16:
                this.explain = "콘체타로 15분 생존";
                this.obtain = "검은 드릴";
                break;

            case 17:
                this.explain = "지아순타로 15분 생존";
                this.obtain = "성스로운 바람";
                break;

            case 18:
                this.explain = "아무 캐릭터로 20분 생존";
                this.obtain = "오망성";
                break;

            case 19:
                this.explain = "크로치로 20분 생존";
                this.obtain = "티라미수";
                break;

            case 20:
                this.explain = "10% 이상의 저주를 받고 20분 동안 생존";
                this.obtain = "라마";
                break;

            case 21:
                this.explain = "라마로 30분 생존";
                this.obtain = "미치광이의 두개골";
                break;

            case 22:
                this.explain = "성경 레벨 4까지 올리기";
                this.obtain = "팔 보호대";
                break;

            case 23:
                this.explain = "성수 레벨 4까지 올리기";
                this.obtain = "촛대";
                break;

            case 24:
                this.explain = "불의 지팡이 레벨 4까지 올리기";
                this.obtain = "아르카";
                break;

            case 25:
                this.explain = "번개 반지 레벨 4까지 올리기";
                this.obtain = "포르타";
                break;

            case 26:
                this.explain = "마법 지팡이 레벨 7까지 올리기";
                this.obtain = "복제 반지";
                break;

            case 27:
                this.explain = "하얀 비둘기 레벨 7까지 올리기";
                this.obtain = "검은 비둘기";
                break;

            case 28:
                this.explain = "룬 트레이서 레벨 7까지 올리기";
                this.obtain = "주문 속박기";
                break;

            case 29:
                this.explain = "마늘을 레벨 7까지 올리기";
                this.obtain = "포";
                break;

            case 30:
                this.explain = "오망성을 레벨 7까지 올리기";
                this.obtain = "크리스틴";
                break;

            case 31:
                this.explain = "토로나의 상자 레벨 9 달성";
                this.obtain = "전능";
                break;

            case 32:
                this.explain = "동시에 무기 6가지 진화";
                this.obtain = "토로나의 상자";
                break;

            case 33:
                this.explain = "6개의 무기를 소지";
                this.obtain = "빈 책";
                break;

            case 34:
                this.explain = "누적 1,000의 체력을 회복";
                this.obtain = "시스터 클레리씨";
                break;

            case 35:
                this.explain = "한 게임에서 5,000골드 획득";
                this.obtain = "돔마리오";
                break;

            case 36:
                this.explain = "갈로 또는 디바노로 30분 생존합니다";
                this.obtain = "팔찌";
                break;

            case 37:
                this.explain = "빛 물체 20개 파괴";
                this.obtain = "불의 지팡이";
                break;

            case 38:
                this.explain = "치킨 5개 발견";
                this.obtain = "마늘";
                break;

            case 39:
                this.explain = "작은 클로버 발견";
                this.obtain = "클로버";
                break;

            case 40:
                this.explain = "흡입기 발견";
                this.obtain = "매혹구";
                break;

            case 41:
                this.explain = "회중시계 발견";
                this.obtain = "시곗바늘";
                break;

            case 42:
                this.explain = "묵주 발견";
                this.obtain = "십자가";
                break;

            case 43:
                this.explain = "돌가면 발견";
                this.obtain = "돌가면";
                break;

            case 44:
                this.explain = "그림 그리모어 발견";
                this.obtain = "진화 목록";
                break;

            case 45:
                this.explain = "매직 뱅어 발견";
                this.obtain = "음악 선택";
                break;

            case 46:
                this.explain = "은하수 지도 발견";
                this.obtain = "일시정지 메뉴 지도";
                break;

            case 47:
                this.explain = "괴물 도감을 찾습니다";
                this.obtain = "도감입니다";
                break;

            case 48:
                this.explain = "마녀의 눈물 발견";
                this.obtain = "단축 모드";
                break;

            case 49:
                this.explain = "란도마조 발견";
                this.obtain = "아르카나";
                break;

            case 50:
                this.explain = "유리 가면을 발견해 구매합니다";
                this.obtain = "상인";
                break;

            case 51:
                this.explain = "노란색 신호 발견";
                this.obtain = "숨겨진 마법 유물";
                break;

            case 52:
                this.explain = "모르베인의 금지된 두루마리를 찾습니다";
                this.obtain = "???";
                break;

            case 53:
                this.explain = "위대한 복음을 찾습니다";
                this.obtain = "한계돌파";
                break;

            case 54:
                this.explain = "모든 스테이지에서 모든 일반 유물을 획득합니다";
                this.obtain = "에우다이모니아 M";
                break;

            case 55:
                this.explain = "그라시아의 거울을 획득합니다";
                this.obtain = "반전 모드입니다";
                break;

            case 56:
                this.explain = "일곱 번째 나팔을 획득합니다";
                this.obtain = "무한 모드입니다";
                break;

            case 57:
                this.explain = "광기의 숲에서 관을 찾아서 열기";
                this.obtain = "푸냘라";
                break;

            case 58:
                this.explain = "화려한 도서관에서 관을 찾아서 열기";
                this.obtain = "지오반나";
                break;

            case 59:
                this.explain = "유제품 공장에서 관을 찾아 열기";
                this.obtain = "포페아";
                break;

            case 60:
                this.explain = "갈로의 탑에서 관을 찾아 열기";
                this.obtain = "콘체타";
                break;

            case 61:
                this.explain = "마그나 예배당에서 관을 찾아 열기";
                this.obtain = "지아순타";
                break;

            case 62:
                this.explain = "누적 3,000마리 스켈레톤 처치";
                this.obtain = "모르타치오";
                break;

            case 63:
                this.explain = "누적 3,000마리 라이온 헤드 처치";
                this.obtain = "야타 카발로";
                break;

            case 64:
                this.explain = "누적 3,000마리 우유의 정령 처치";
                this.obtain = "비앙카 람바";
                break;

            case 65:
                this.explain = "누적 3,000마리 드래곤 쉬림프 처치";
                this.obtain = "오'솔레";
                break;

            case 66:
                this.explain = "누적 6,000마리 스테이지 킬러 처치";
                this.obtain = "경 암브로조";
                break;

            case 67:
                this.explain = "누적 3,000마리 적을 처치";
                this.obtain = "번개 반지";
                break;

            case 68:
                this.explain = "누적 100,000마리의 적을 처치";
                this.obtain = "크로치";
                break;

            case 69:
                this.explain = "시그마 여왕으로 한 게임에서 적을 100,000명 처치합니다";
                this.obtain = "승리의 검";
                break;

            case 70:
                this.explain = "광기의 숲에서 거대한 푸른 비너스를 쓰러뜨리기";
                this.obtain = "하이퍼 광기의 숲";
                break;

            case 71:
                this.explain = "화려한 도서관에서 네스페리트를 쓰러뜨리기";
                this.obtain = "하이퍼 화려한 도서관";
                break;

            case 72:
                this.explain = "유제품 공장에서 소드 가디언을 쓰러뜨리기";
                this.obtain = "하이퍼 유제품 공";
                break;

            case 73:
                this.explain = "갈로의 탑에서 자이언트 크랩 쓰러뜨리기";
                this.obtain = "하이퍼 갈로의 탑";
                break;

            case 74:
                this.explain = "마그나 예배당에서 트리나크리아를 쓰러뜨리기";
                this.obtain = "하이퍼 마그나 예배당";
                break;

            case 75:
                this.explain = "보통 스테이지에서 하이퍼모드를 잠금 해제합니다";
                this.obtain = "하이퍼 일 몰리제";
                break;

            case 76:
                this.explain = "2개의 일반 스테이지에서 하이퍼모드 잠금 해제";
                this.obtain = "하이퍼 푸른 초원";
                break;

            case 77:
                this.explain = "3개의 일반 스테이지에서 하이퍼모드 잠금 해제";
                this.obtain = "하이퍼 뼈의 대지";
                break;

            case 78:
                this.explain = "4개의 일반 스테이지에서 하이퍼모드를 달성합니다";
                this.obtain = "하이퍼 달빛";
                break;

            case 79:
                this.explain = "5개의 일반 스테이지에서 하이퍼모드를 잠금 해제합니다";
                this.obtain = "하이퍼 보스 래쉬";
                break;

            case 80:
                this.explain = "마그나 예배당에서 마지막 적을 처치합니다";
                this.obtain = "게임 킬러";
                break;

            case 81:
                this.explain = "푸냘라로 레벨 50에 도달합니다";
                this.obtain = "I - 쌍둥이 자리";
                break;

            case 82:
                this.explain = "돔마리오로 레벨 50에 도달합니다";
                this.obtain = "II - 황혼의 진혼곡";
                break;

            case 83:
                this.explain = "포르타로 레벨 50에 도달합니다";
                this.obtain = "III - 공주의 비련";
                break;

            case 84:
                this.explain = "크로치로 레벨 50에 도달합니다";
                this.obtain = "IV - 각성";
                break;

            case 85:
                this.explain = "지오바나로 레벨 50에 도달합니다";
                this.obtain = "V - 혼돈이 가득한 어두운 밤";
                break;

            case 86:
                this.explain = "란도마조 발견";
                this.obtain = "VI - 치유의 사라반드";
                break;

            case 87:
                this.explain = "젠나로로 레벨 50에 도달합니다";
                this.obtain = "VII - 푸른 강철의 의지";
                break;

            case 88:
                this.explain = "광기의 숲에서 31분 생존";
                this.obtain = "VIII - 광기의 그루브";
                break;

            case 89:
                this.explain = "시스터 클레리씨로 레벨 50에 도달합니다";
                this.obtain = "IX - 신성한 혈통";
                break;

            case 90:
                this.explain = "안토니오로 레벨 50에 도달합니다";
                this.obtain = "X - 근원";
                break;

            case 91:
                this.explain = "이멜다로 레벨 50에 도달합니다";
                this.obtain = "XI - 진주의 왈츠";
                break;

            case 92:
                this.explain = "갈로의 탑에서 31분 동안 생존합니다";
                this.obtain = "XII - 한도 초과";
                break;

            case 93:
                this.explain = "크리스틴으로 레벨 50에 도달합니다";
                this.obtain = "XIII - 사악한 계절";
                break;

            case 94:
                this.explain = "파스콸리나로 레벨 50에 도달합니다";
                this.obtain = "XIV - 수정감";
                break;

            case 95:
                this.explain = "화려한 도서관에서 31분을 생존";
                this.obtain = "XV - 황금 디스코";
                break;

            case 96:
                this.explain = "라마로 레벨 50에 도달합니다";
                this.obtain = "XVI - 절단";
                break;

            case 97:
                this.explain = "포페아로 레벨 50에 도달합니다";
                this.obtain = "XVII - 잃어버린 그림";
                break;

            case 98:
                this.explain = "콘체타로 레벨 50에 도달합니다";
                this.obtain = "XVIII - 환상의 춤";
                break;

            case 99:
                this.explain = "아르카로 레벨 50에 도달합니다";
                this.obtain = "XIX - 불의 심장";
                break;

            case 100:
                this.explain = "유제품 공장에서 31분을 생존";
                this.obtain = "XX - 고요한 옛 성역";
                break;

            case 101:
                this.explain = "포로 레벨 50에 도달합니다";
                this.obtain = "   XXI - 블러드 아스트로 노미아";
                break;

            case 102:
                this.explain = "모르타치오로 레벨 80 달성";
                this.obtain = "새로고침";
                break;

            case 103:
                this.explain = "야타 카발로 레벨 80 달성";
                this.obtain = "새로고침";
                break;

            case 104:
                this.explain = "비앙카 람바로 레벨 80 달성";
                this.obtain = "새로고침";
                break;

            case 105:
                this.explain = "오'솔레미오로 레벨 80 달성";
                this.obtain = "새로고침";
                break;

            case 106:
                this.explain = "암브로조 경로 레벨 80 달성";
                this.obtain = "새로고침";
                break;

            case 107:
                this.explain = "일 몰리제에서 15분 생존";
                this.obtain = "건너뛰기";
                break;

            case 108:
                this.explain = "달빛에서 15분 생존";
                this.obtain = "건너뛰기";
                break;

            case 109:
                this.explain = "푸른 초원에서 30분 생존";
                this.obtain = "건너뛰기";
                break;

            case 110:
                this.explain = "뼈의 대지에서 30분 생존";
                this.obtain = "건너뛰기";
                break;

            case 111:
                this.explain = "보스 래쉬에서 15분 동안 생존합니다";
                this.obtain = "건너뛰기";
                break;

            case 112:
                this.explain = "컬렉션에 50개의 항목을 채우기";
                this.obtain = "캐릭터 커스터마이징";
                break;

            case 113:
                this.explain = "컬렉션에 60개의 항목을 채우기";
                this.obtain = "지우기";
                break;

            case 114:
                this.explain = "컬렉션에 70개의 항목을 채우기";
                this.obtain = "지우기";
                break;

            case 115:
                this.explain = "컬렉션에 80개의 항목을 채우기";
                this.obtain = "지우기";
                break;

            case 116:
                this.explain = "컬렉션에 90개의 항목을 채우기";
                this.obtain = "지우기";
                break;

            case 117:
                this.explain = "컬렉션에 100개의 항목을 채우기";
                this.obtain = "지우기";
                break;

            case 118:
                this.explain = "한 게임에서 10개 이상의 무기를 지웁니다";
                this.obtain = "봉인";
                break;

            case 119:
                this.explain = "컬렉션을 완성합니다";
                this.obtain = "여왕 시그마";
                break;

            case 120:
                this.explain = "마지막 불꽃놀이를 봅니다";
                this.obtain = "위대한 기념제";
                break;

            case 121:
                this.explain = "무한 회랑 획득";
                this.obtain = "이구아나 갈로";
                break;

            case 122:
                this.explain = "진홍의 망토 획득";
                this.obtain = "디바노";
                break;

            case 123:
                this.explain = "채찍 진화";
                this.obtain = "골드 500개";
                break;

            case 124:
                this.explain = "마법 지팡이 진화";
                this.obtain = "골드 500개";
                break;

            case 125:
                this.explain = "단검 진화";
                this.obtain = "골드 500개";
                break;

            case 126:
                this.explain = "도끼 진화";
                this.obtain = "골드 500개";
                break;

            case 127:
                this.explain = "성수 진화";
                this.obtain = "골드 500개";
                break;

            case 128:
                this.explain = "번개 반지 진화 진화";
                this.obtain = "골드 500개";
                break;

            case 129:
                this.explain = "기도문 진화";
                this.obtain = "골드 500개";
                break;

            case 130:
                this.explain = "십자가 진화";
                this.obtain = "골드 500개";
                break;

            case 131:
                this.explain = "불의 지팡이 진화";
                this.obtain = "골드 500개";
                break;
                
            case 132:
                this.explain = "마늘 진화";
                this.obtain = "골드 500개";
                break;
                
            case 133:
                this.explain = "룬 트레이서 진화";
                this.obtain = "골드 500개";
                break;
                
            case 134:
                this.explain = "오망성 진화 진화";
                this.obtain = "골드 500개";
                break;
                
            case 135:
                this.explain = "하얀 비둘기와 검은 비둘기 융합";
                this.obtain = "골드 500개";
                break;
                
            case 136:
                this.explain = "피에라 데 투펠로와 에잇 더 스패로우 진화";
                this.obtain = "골드 500개";
                break;
                
            case 137:
                this.explain = "마녀의 고양이 진화";
                this.obtain = "골드 500개";
                break;
                
            case 138:
                this.explain = "마나의 노래 진화";
                this.obtain = "골드 500개";
                break;
                
            case 139:
                this.explain = "검은 드릴 진화";
                this.obtain = "골드 500개";
                break;
                
            case 140:
                this.explain = "성스러운 바람과 피눈물 융합";
                this.obtain = "골드 500개";
                break;
                
            case 141:
                this.explain = "팔찌와 이중 팔찌를 진화";
                this.obtain = "골드 500개";
                break;
                
            case 142:
                this.explain = "모든 일반 진화 및 조합을 발견합니다";
                this.obtain = "사탕상자";
                break;
                
            case 143:
                this.explain = "문스펠산에서 관을 찾아서 엽니다";
                this.obtain = "미앙 문스펠";
                break;
                
            case 144:
                this.explain = "미앙 문스펠로 15분 생존합니다";
                this.obtain = "은색 바람";
                break;
                
            case 145:
                this.explain = "은색 바람을 진화합니다";
                this.obtain = "멘야 문스펠";
                break;
                
            case 146:
                this.explain = "멘야 문스펠로 15분 생존합니다";
                this.obtain = "사계절";
                break;
                
            case 147:
                this.explain = "사계절을 진화합니다";
                this.obtain = "슈토 문스펠";
                break;
                
            case 148:
                this.explain = "슈토 문스펠로 15분 생존합니다";
                this.obtain = "밤 소환";
                break;
                
            case 149:
                this.explain = "밤 소환을 진화합니다";
                this.obtain = "바비 온나";
                break;
                
            case 150:
                this.explain = "바비 온나로 15분 생존합니다";
                this.obtain = "신기루 로브";
                break;
                
            case 151:
                this.explain = "신기루 로브를 진화합니다";
                this.obtain = "맥코이 오니";
                break;
                
            case 152:
                this.explain = "맥코이 오니로 15분 생존합니다";
                this.obtain = "108 보체";
                break;
                
            case 153:
                this.explain = "멘야 문스펠로 한 게임에서 적을 100,000명 처치합니다";
                this.obtain = "강력한 멘야 문스펠";
                break;
                
            case 154:
                this.explain = "슈토 문스펠로 한 게임에서 적을 100,000명 처치합니다";
                this.obtain = "강력한 슈토 문스펠";
                break;
                
            case 155:
                this.explain = "갓파를 6,000마리 처치합니";
                this.obtain = "가베트오니";
                break;
                
            case 156:
                this.explain = "밤의 검을 발견합니다";
                this.obtain = "밤의 검";
                break;
                
            case 157:
                this.explain = "밤의 검을 진화합니다";
                this.obtain = "골드 50,000개";
                break;
                
            case 158:
                this.explain = "밀레 볼레 블루 진화합니다";
                this.obtain = "골드 50,000개";
                break;
                
            case 159:
                this.explain = "문스펠산에서 오로치마리오를 처치합니다";
                this.obtain = "하이퍼 문스펠산";
                break;
        }

        if (UserInfo.userAchi[achi-1]) {
            achiToggle.GetComponent<Toggle>().isOn = true;
        }
        thisAchiName.text = this.explain;
    }

    // Update is called once per frame
    void Update(){
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (UserInfo.userAchi[achi-1]) {
            achiName.text = "획득";
            achiImageBG.color = new Color(0f, 1f, 1f, 1f);
        }
        else{
            achiName.text = "";
            achiImageBG.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        
        achiExplain.text = this.explain;
        achiObtain.text = this.obtain;
        achiImage.GetComponent<Image>().sprite = thisAchiIamge.GetComponent<Image>().sprite;
    }

}
